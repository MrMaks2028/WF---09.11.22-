﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF____09._11._22_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.BlanchedAlmond;
        }

        private void myFormSize()
        {
            var actualSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
            actualSize.Width /= 4;
            actualSize.Height /= 5;
            this.MaximumSize = this.MinimumSize = actualSize;
        }
        private void runString()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\form.txt";
            var reader = new StreamReader(path);
            label1.Text = reader.ReadLine();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //var mSize = SystemInformation.PrimaryMonitorSize;
            //var actualSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
            //int width = mSize.Width;
            //int height = mSize.Height;
            //labelResolution.Text = $"\n Активная область:" + $"\nШирина = {actualSize.Width}, высота = {actualSize.Height}";
           // myFormSize();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            myFormSize();
            labelResolution.Text += $"\n Размер окна:" +
                $"\nШирина = {this.Width}, высота = {this.Height}";
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "c_sharp files (*.cs)|*.cs|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                var fileStream = openFileDialog.OpenFile();

                //using (StreamReader reader = new StreamReader(fileStream))
                //{
                //    fileContent = reader.ReadToEnd();
                //}
                StreamReader reader = new StreamReader(fileStream);
                fileContent = reader.ReadToEnd();
                reader.Close();
            }
            MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }
        private void transform()
        {
            var height = this.Height;
            var width = this.Width;
            System.Drawing.Drawing2D.GraphicsPath myPath = new System.Drawing.Drawing2D.GraphicsPath();
            myPath.AddPolygon(new Point[] { new Point(width/3, 0), //Верхняя левая точка
                                            new Point((width/3)*2, 0), //Верхняя правая точка
                                            new Point(width, height/3),
                                            new Point(width, (height/3)*2),
                                            new Point((width/3)*2, height), //Нижняя правая точка
                                            new Point(width/3, height), //Нижняя левая точка
                                            new Point(0, (height/3)*2),
                                            new Point(0, height/3)
            });
            Region myRegiion = new Region(myPath);
            this.Region = myRegiion;
        }

        private void buttonTransform_MouseClick(object sender, MouseEventArgs e)
        {
            transform();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            runString();
            if (label1.Left > -label1.Width)
            {
                label1.Left -= 5;
            }
            else
            {
                label1.Left = this.Width;
            }
        }
    }
}
