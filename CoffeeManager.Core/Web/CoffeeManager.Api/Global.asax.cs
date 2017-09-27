using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoffeeManager.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected string fileNameBase = "f:\\temp\\log-";

        protected string ext = "log";

        // One file name per day

        protected string FileName => String.Format("{0}{1}.{2}", fileNameBase, DateTime.Now.ToString("yyyy-MM-dd"), ext);



        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Creates a unique id to match Rquests with Responses

            //string id = String.Format("Id: {0} Uri: {1}", Guid.NewGuid(), Request.Url);

            //FilterSaveLog input = new FilterSaveLog(HttpContext.Current, Request.Filter, FileName, id);

            //input.SetFilter(false);

            //Request.Filter = input;

            //FilterSaveLog output = new FilterSaveLog(HttpContext.Current, Response.Filter, FileName, id);
            //output.SetFilter(true);
            //Response.Filter = output;
        }
    }



    class FilterSaveLog : Stream

    {



        protected static string fileNameGlobal = null;

        protected string fileName = null;



        protected static object writeLock = null;

        protected Stream sinkStream;

        protected bool inDisk;

        protected bool isClosed;

        protected string id;

        protected bool isResponse;

        protected HttpContext context;



        public FilterSaveLog(HttpContext Context, Stream Sink, string FileName, string Id)
        {
            // One lock per file nam
            if (String.IsNullOrWhiteSpace(fileNameGlobal) || fileNameGlobal.ToUpper() != fileNameGlobal.ToUpper())
            {
                fileNameGlobal = FileName;
                writeLock = new object();
            }

            context = Context;

            fileName = FileName;

            id = Id;

            sinkStream = Sink;

            inDisk = false;

            isClosed = false;

        }



        public void SetFilter(bool IsResponse)
        {
            isResponse = IsResponse;

            id = (isResponse ? "Reponse " : "Request ") + id;
            //

            // For Request only read the incoming stream and log it as it will not be "filtered" for a WCF request

            //

            if (!IsResponse)

            {

                AppendToFile(String.Format("at {0} --------------------------------------------", DateTime.Now));

                AppendToFile(id);



                if (context.Request.InputStream.Length > 0)

                {

                    context.Request.InputStream.Position = 0;

                    byte[] rawBytes = new byte[context.Request.InputStream.Length];

                    context.Request.InputStream.Read(rawBytes, 0, rawBytes.Length);

                    context.Request.InputStream.Position = 0;



                    AppendToFile(rawBytes);

                }

                else

                {

                    AppendToFile("(no body)");

                }

            }



        }



        public void AppendToFile(string Text)

        {

            byte[] strArray = Encoding.UTF8.GetBytes(Text);

            AppendToFile(strArray);



        }



        public void AppendToFile(byte[] RawBytes)

        {

            bool myLock = System.Threading.Monitor.TryEnter(writeLock, 100);





            if (myLock)

            {

                try

                {



                    using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))

                    {

                        stream.Position = stream.Length;

                        stream.Write(RawBytes, 0, RawBytes.Length);

                        stream.WriteByte(13);

                        stream.WriteByte(10);



                    }



                }

                catch (Exception ex)

                {

                    string str = string.Format("Unable to create log. Type: {0} Message: {1}\nStack:{2}", ex, ex.Message, ex.StackTrace);

                    System.Diagnostics.Debug.WriteLine(str);

                    System.Diagnostics.Debug.Flush();





                }

                finally

                {

                    System.Threading.Monitor.Exit(writeLock);





                }

            }





        }





        public override bool CanRead

        {

            get { return sinkStream.CanRead; }

        }



        public override bool CanSeek

        {

            get { return sinkStream.CanSeek; }

        }



        public override bool CanWrite

        {

            get { return sinkStream.CanWrite; }

        }



        public override long Length

        {

            get

            {

                return sinkStream.Length;

            }

        }



        public override long Position

        {

            get { return sinkStream.Position; }

            set { sinkStream.Position = value; }

        }





        //

        public override int Read(byte[] buffer, int offset, int count)

        {

            int c = sinkStream.Read(buffer, offset, count);

            return c;

        }



        public override long Seek(long offset, System.IO.SeekOrigin direction)

        {

            return sinkStream.Seek(offset, direction);

        }



        public override void SetLength(long length)

        {

            sinkStream.SetLength(length);

        }



        public override void Close()

        {



            sinkStream.Close();

            isClosed = true;

        }



        public override void Flush()

        {



            sinkStream.Flush();

        }



        // For streamed responses (i.e. not buffered) there will be more than one Response (but the id will match the Request)

        public override void Write(byte[] buffer, int offset, int count)

        {

            sinkStream.Write(buffer, offset, count);

            AppendToFile(String.Format("at {0} --------------------------------------------", DateTime.Now));

            AppendToFile(id);

            AppendToFile(buffer);

        }



    }

}
    

