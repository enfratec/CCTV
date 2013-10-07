using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Threading;
using System.IO;

namespace CCTV_Client
{
    public partial class CCTVClient : Form
    {
        private Capture cam;
        private volatile bool videoInProgress;
        private volatile bool isConnected;
        private ClientSocket clientSocket;
        private Thread videoThread;
        delegate void setThreadedButtonLabelCallback(String text);

        public CCTVClient()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.CCTVClient_Closing);

            clientSocket = new ClientSocket();

            videoThread = null;
            isConnected = false;
            videoInProgress = false;

            //camera: start
            try
            {
                cam = new Capture();
                cam.FlipHorizontal = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Camera:\t\n" + exc.Message.ToString());
            }
        }

        private bool startConnection()
        {
            try {

                if (this.IPBox.Text.Length > 0 && this.PortBox.Text.Length > 0)
                {
                    clientSocket.ip = this.IPBox.Text.ToString();
                    clientSocket.port = this.PortBox.Text.ToString();
                    clientSocket.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.socket.Connect(clientSocket.ip, Int32.Parse(clientSocket.port));
                    return true;
                }
                else
                {
                    if( this.IPBox.Text.Length <= 0 )
                    {
                        MessageBox.Show("No server address!");
                    }
                    else if(this.PortBox.Text.Length <= 0)
                    {
                        MessageBox.Show("No server port number!");
                    }
                    return false;
                }
            }
            catch(Exception exc) 
            {
                MessageBox.Show("StartConnection:\t\n" + exc.Message.ToString());
                return false;
            }   
        }

        private void processFrame()
        {
            try
            {
                while (videoInProgress) 
                {                        
                    //get current frame
                    Image<Bgr, Byte> frame = cam.QuerySmallFrame();
                    CamImageBox.Image = frame;
                    Bitmap tmp = frame.ToBitmap();
                    MemoryStream stream = new System.IO.MemoryStream();
                    tmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                    //sending current frame - synchronously
                    clientSocket.sendFrame( stream.ToArray() );

                    //sending current frame - asynchronous
                    //clientSocket.sendFrameAsynchronous( stream.ToArray() );
                }
            }
            //exception from camera or network: close connection
            catch (Exception exc)
            {
                MessageBox.Show("ProcessFrame:\t\n" + exc.Message.ToString());
                videoInProgress = !videoInProgress;
                clientSocket.socket.Close();
                CamImageBox.Image = null;
                setThreadedButtonLabel("Start");
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (videoInProgress)
            {
                setThreadedButtonLabel("Start");
            }
            else
            {
                if (!isConnected)
                {
                    isConnected = startConnection();
                }
                if (isConnected)
                {
                    //start worker thread (for video and sending)
                    videoThread = new Thread(processFrame);
                    videoThread.IsBackground = true;
                    videoThread.Start();
                    setThreadedButtonLabel("Stop");
                }
            }
            videoInProgress = !videoInProgress;
        }

        private void CCTVClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {   
            videoInProgress = !videoInProgress;
            if( isConnected )
            {
                clientSocket.socket.Close();
            }
            Application.Exit();
        }

        private void setThreadedButtonLabel(String text)
        {
            if( this.StartButton.InvokeRequired )
            {
                setThreadedButtonLabelCallback buttonLabelCallback = new setThreadedButtonLabelCallback(setThreadedButtonLabel);
                this.Invoke(buttonLabelCallback, text);
            }
            else
            {
                this.StartButton.Text = text;
            }
        }

    }
}
