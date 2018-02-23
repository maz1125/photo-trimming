using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoTrimminger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private static string separator = "=========================================";
        private static int ORDER_NO_LABEL_END = 11;
        private static int SIZE_LABEL_END = 11;
        private static int TITLE_LABEL_END = 14;
        private static int COUNT_LABEL_END = 13;
        private static int ROTATE_LABEL_END = 15;
        private static int TRIMMING_START_LABEL_END = 25;
        private static int TRIMMING_END_LABEL_END = 23;
        private static int TRIMMING_SIZE_LABEL_END = 15;
        private static int DATE_TIME_PRINT_LABEL_END = 17;
        private static int DATE_TIME_LABEL_END = 10;

        private void selectTargetFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void writeLog(string logText)
        {
            consoleTextBox.AppendText("[" + System.DateTime.Now.ToString() + "]" + logText + Environment.NewLine);
        }

        private void trimmingBtn_Click(object sender, EventArgs e)
        {
            writeLog("-----Trimming Start-----");
            trimmingBtn.Enabled = false;
            try
            {
                executeTrimming(textBox1.Text);
                writeLog("-----正常終了-----");

            } catch (Exception exception)
            {
                writeLog("-----異常終了-----");
            } finally
            {
                trimmingBtn.Enabled = true;
            }

        }

        private void executeTrimming(string folderPath)
        {
            Order order = createOrderObject(folderPath);
            loadOrderFile(order);
            loadOrderPictures(order);
            createSaveFolder(order);
            foreach (OrderDetail detail in order.details)
            {
                executeRotate(detail);
                executeTrimming(detail);
                saveEditedFile(order, detail);
                disposeOrderDetail(detail);
            }
            outputOrderInformation(order);

        }

        private Order createOrderObject(string folderPath)
        {
            Order result = new PhotoTrimminger.Order();
            result.targetFolderPath = folderPath;
            result.saveFolderPath = folderPath + "\\" + "printTarget";
            result.targetOrderText = folderPath + "\\" + "Order.txt";
            return result;
        }

        private void loadOrderFile(Order order)
        {
            System.IO.StreamReader streamReader = null;
            try
            {
                TargetDetail targetDetail = null;
                List<OrderDetail> details = new List<OrderDetail>();
                streamReader = (new System.IO.StreamReader(order.targetOrderText, Encoding.Default));
                while (streamReader.Peek() >= 0)
                {
                    string textLine = streamReader.ReadLine();
                    if (textLine.StartsWith("Order No."))
                    {
                        string orderNo = textLine.Remove(0, ORDER_NO_LABEL_END);
                        order.orderNo = orderNo;

                    } else if (textLine.StartsWith("Size"))
                    {
                        string size = textLine.Remove(0, SIZE_LABEL_END);
                        order.size = size;
                    } else if (textLine.StartsWith(separator))
                    {
                        if (targetDetail != null)
                        {
                            OrderDetail orderDetail = createOrderDetail(targetDetail);
                            details.Add(orderDetail);
                        }
                    } else if (textLine.StartsWith("Photo Title"))
                    {
                        targetDetail = new TargetDetail();
                        targetDetail.title = textLine.Remove(0, TITLE_LABEL_END);

                    } else if (textLine.StartsWith("Print Count"))
                    {
                        targetDetail.count = textLine.Remove(0, COUNT_LABEL_END);

                    } else if (textLine.StartsWith("Right rotated"))
                    {
                        targetDetail.rotate = textLine.Remove(0, ROTATE_LABEL_END);

                    } else if (textLine.StartsWith("Trimming Start Position"))
                    {
                        targetDetail.trimmingStart = textLine.Remove(0, TRIMMING_START_LABEL_END);

                    } else if (textLine.StartsWith("Trimming End Position"))
                    {
                        targetDetail.trimmingEnd = textLine.Remove(0, TRIMMING_END_LABEL_END);

                    } else if (textLine.StartsWith("Trimming Size"))
                    {
                        targetDetail.trimmingSize = textLine.Remove(0, TRIMMING_SIZE_LABEL_END);

                    } else if (textLine.StartsWith("Date Time Print"))
                    {
                        targetDetail.dateTimePrint = textLine.Remove(0, DATE_TIME_PRINT_LABEL_END);

                    } else if (textLine.StartsWith("DateTime"))
                    {
                        targetDetail.dateTime = textLine.Remove(0, DATE_TIME_LABEL_END);

                    }
                }
                order.details = details;
            } catch(Exception e)
            {
                writeLog("order.txtの読み込みでエラーが発生しました" + e.Message);
                throw e;

            }
            finally
            {
                streamReader.Close();

            }

        }

        private OrderDetail createOrderDetail(TargetDetail target)
        {
            OrderDetail result = new OrderDetail();
            result.originalName = target.title;
            result.count = target.count;
            result.rotation = int.Parse(target.rotate);
            string[] trimmingStart = target.trimmingStart.Split(',');
            result.trimmingLeft = int.Parse(trimmingStart[0]);
            result.trimmingTop = int.Parse(trimmingStart[1]);
            string[] trimmingSize = target.trimmingSize.Split(',');
            result.trimmingWidth = int.Parse(trimmingSize[0]);
            result.trimmingHeight = int.Parse(trimmingSize[1]);
            return result;
        }


        private void loadOrderPictures(Order order)
        {
            try
            {
                foreach (OrderDetail detail in order.details)
                {
                    string pictureName = detail.originalName;
                    string picturePath = order.targetFolderPath + "\\" + pictureName;
                    System.Drawing.Image targetPicture = System.Drawing.Image.FromFile(picturePath);
                    detail.originalImage = targetPicture;
                }

            } catch (Exception e)
            {
                writeLog("写真の読み込みでエラーが発生しました" + e.Message);
                throw e;
            }

        }


        private void createSaveFolder(Order order)
        {
            try
            {
                System.IO.Directory.CreateDirectory(order.saveFolderPath);
            }
            catch(Exception e)
            {
                writeLog("フォルダ作成でエラーが発生しました" + e.Message);
                throw e;
            }

        }

        private void executeRotate(OrderDetail detail)
        {
            try
            {
                Image editedImage = (Image)detail.originalImage.Clone();
                int rotate = detail.rotation;
                switch (rotate)
                {
                    case 0:
                        break;
                    case 90:
                        editedImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 180:
                        editedImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 270:
                        editedImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    default:
                        break;
                }
                detail.editedImage = editedImage;

            } catch(Exception e)
            {
                writeLog("Rotate作業でエラーが発生しました" + e.Message);
                throw e;
            }

        }

        private void executeTrimming(OrderDetail detail)
        {
            try
            {
                Bitmap targetBitmap = new Bitmap(detail.editedImage);
                if (detail.trimmingWidth != 0 && detail.trimmingHeight != 0)
                {
                    Rectangle rect = new Rectangle(detail.trimmingLeft, detail.trimmingTop, detail.trimmingWidth, detail.trimmingHeight);
                    Bitmap editedBitmap = targetBitmap.Clone(rect, targetBitmap.PixelFormat);
                    detail.editedImage = editedBitmap;
                }

            } catch(Exception e)
            {
                writeLog("トリミング作業でエラーが発生しました" + e.Message);
                throw e;
            }
        }

        private void saveEditedFile(Order order , OrderDetail detail)
        {
            try
            {
                string picturePath = order.saveFolderPath + "\\" + detail.originalName;
                detail.editedImage.Save(picturePath);
            }catch(Exception e)
            {
                writeLog("編集後写真の保存で失敗しました" + e.Message);
                throw e;
            }
        }

        private void disposeOrderDetail(OrderDetail detail)
        {
            try
            {
                detail.originalImage.Dispose();
                detail.editedImage.Dispose();
            } catch (Exception e)
            {
                writeLog("Dispose作業でエラーが発生しました" + e.Message);
                throw e;
            }

        }

        private void outputOrderInformation(Order order)
        {
            System.IO.StreamWriter writer = null;
            try
            {
                string orderInformationPath = order.saveFolderPath + "\\" + "OrderInformation.txt";
                writer = new System.IO.StreamWriter(orderInformationPath, false, System.Text.Encoding.GetEncoding("utf-8"));
                writer.WriteLine("Size : " + order.size);
                foreach (OrderDetail detail in order.details)
                {
                    writer.WriteLine(detail.originalName + " : " + detail.count);
                }

            } catch(Exception e)
            {
                writeLog("OrderInformation作業でエラーが発生しました" + e.Message);
                throw e;
            } finally {
                writer.Close();
            }
        }

        public void execute(string folderPath)
        {
            executeTrimming(folderPath);

        }
    }
}
