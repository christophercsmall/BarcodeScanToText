using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreScanner;

namespace BarcodeScanToText
{
    public partial class Form1 : Form
    {
        CCoreScannerClass cCoreScannerClass;

        public Form1()
        {
            InitializeComponent();
        }

        public void OnBarcodeEvent(short eventType, ref string scanData)
        {
            string barcode = scanData;
            this.Invoke((MethodInvoker)delegate { scannedData.Text = barcode; });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                cCoreScannerClass = new CoreScanner.CCoreScannerClass();

                short[] scannerTypes = new short[1];
                scannerTypes[0] = 1;
                short numberOfScannerTypes = 1;
                int status;

                cCoreScannerClass.Open(0, scannerTypes, numberOfScannerTypes, out status);

                cCoreScannerClass.BarcodeEvent += new _ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);

                short numberOfScanners;
                int[] connectedScannerIDList = new int[10];
                string outXML;

                cCoreScannerClass.GetScanners(out numberOfScanners, connectedScannerIDList, out outXML, out status);

                int opcode = 1001;
                string inXML = "<inArgs>" +
                                    "<cmdArgs>" +
                                        "<arg-int>1</arg-int>" +
                                        "<arg-int>1</arg-int>" +
                                    "</cmdArgs>" +
                                "</inArgs>";

                cCoreScannerClass.ExecCommand(opcode, ref inXML, out outXML, out status);


            }
            catch (Exception exp)
            {
                msg.Text = exp.ToString();
            }
            //cCoreScannerClass = new CoreScanner.CCoreScannerClass();

            //short numberOfScanner;
            //int[] connectedScannerIDList = new int[10];
            //string outXML;
            //cCoreScannerClass.GetScanners(out numberOfScanner, connectedScannerIDList, out outXML, out status);
            //msg.Text = outXML;


            //if (status == 0)
            //{
            //    msg.Text = "CoreScanner API: Open Successful";
            //    //Console.WriteLine("CoreScanner API: Open Successful");
            //}
            //else
            //{
            //    msg.Text = "CoreScanner API: Open Failed";
            //    //Console.WriteLine("CoreScanner API: Open Failed");
            //}



        }

    public void button1_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}
