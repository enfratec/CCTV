using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.IO;

namespace CCTV_Client
{
    class ClientSocket
    {
        public String ip;
        public String port;
        public Socket socket;

        public void sendFrame(byte[] frame)
        {
            Int32 frameSize = 0;

            if ( frame != null ) 
            {
                //send size of frame
                frameSize = frame.Length;
                socket.Send(BitConverter.GetBytes(frameSize).ToArray());

                //send frame
                socket.Send(frame);
            }
        }

        public void sendFrameAsynchronous(byte[] frame)
        {
            Int32 frameSize = 0;
            if (frame != null)
            {
                frameSize = frame.Length;
                socket.BeginSend(BitConverter.GetBytes(frameSize).ToArray(), 0, 4, 0, new AsyncCallback(sendCallback), socket);
                socket.BeginSend(frame, 0, frameSize, 0, new AsyncCallback(sendCallback), socket);
            }
        }

        public void sendCallback(IAsyncResult ar)
        {
            Socket socketFd = (Socket)ar.AsyncState;
            socketFd.EndSend(ar);
        }
    }
}
