
namespace RotatePicture
{
    partial class HomeForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.create_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.originPictureAngle_tbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.finalPictureAngle_tbx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.deltaAngle_tbx = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deletePictures_btn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.playRotatePics_btn = new System.Windows.Forms.Button();
            this.playPics_picb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playPics_picb)).BeginInit();
            this.SuspendLayout();
            // 
            // create_btn
            // 
            this.create_btn.Location = new System.Drawing.Point(356, 288);
            this.create_btn.Name = "create_btn";
            this.create_btn.Size = new System.Drawing.Size(172, 77);
            this.create_btn.TabIndex = 1;
            this.create_btn.Text = "生成旋转后图片";
            this.create_btn.UseVisualStyleBackColor = true;
            this.create_btn.Click += new System.EventHandler(this.Create_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(282, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "输入当前图片的旋转角度";
            // 
            // originPictureAngle_tbx
            // 
            this.originPictureAngle_tbx.Location = new System.Drawing.Point(428, 24);
            this.originPictureAngle_tbx.Name = "originPictureAngle_tbx";
            this.originPictureAngle_tbx.Size = new System.Drawing.Size(86, 23);
            this.originPictureAngle_tbx.TabIndex = 6;
            this.originPictureAngle_tbx.TextChanged += new System.EventHandler(this.CurrentPictureAngle_tbx_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(543, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 27);
            this.label2.TabIndex = 8;
            this.label2.Text = "图片旋转";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(628, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.finalPictureAngle_tbx);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.deltaAngle_tbx);
            this.groupBox1.Controls.Add(this.create_btn);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.originPictureAngle_tbx);
            this.groupBox1.Location = new System.Drawing.Point(45, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1177, 445);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图片生成";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "最后一张图片的旋转角度";
            // 
            // finalPictureAngle_tbx
            // 
            this.finalPictureAngle_tbx.Location = new System.Drawing.Point(428, 130);
            this.finalPictureAngle_tbx.Name = "finalPictureAngle_tbx";
            this.finalPictureAngle_tbx.Size = new System.Drawing.Size(86, 23);
            this.finalPictureAngle_tbx.TabIndex = 16;
            this.finalPictureAngle_tbx.TextChanged += new System.EventHandler(this.LastPictureAngle_tbx_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "每次变化的°数";
            // 
            // deltaAngle_tbx
            // 
            this.deltaAngle_tbx.Location = new System.Drawing.Point(428, 78);
            this.deltaAngle_tbx.Name = "deltaAngle_tbx";
            this.deltaAngle_tbx.Size = new System.Drawing.Size(86, 23);
            this.deltaAngle_tbx.TabIndex = 14;
            this.deltaAngle_tbx.TextChanged += new System.EventHandler(this.DeltaAngle_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deletePictures_btn);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Controls.Add(this.playRotatePics_btn);
            this.groupBox2.Controls.Add(this.playPics_picb);
            this.groupBox2.Location = new System.Drawing.Point(45, 528);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1177, 473);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "旋转预览";
            // 
            // deletePictures_btn
            // 
            this.deletePictures_btn.Location = new System.Drawing.Point(491, 112);
            this.deletePictures_btn.Name = "deletePictures_btn";
            this.deletePictures_btn.Size = new System.Drawing.Size(75, 33);
            this.deletePictures_btn.TabIndex = 17;
            this.deletePictures_btn.Text = "清空";
            this.deletePictures_btn.UseVisualStyleBackColor = true;
            this.deletePictures_btn.Click += new System.EventHandler(this.DeletePictures_btn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(491, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 35);
            this.button2.TabIndex = 16;
            this.button2.Text = "打开文件位置";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OpenFileLocation_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(54, 22);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 400);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // playRotatePics_btn
            // 
            this.playRotatePics_btn.Location = new System.Drawing.Point(491, 170);
            this.playRotatePics_btn.Name = "playRotatePics_btn";
            this.playRotatePics_btn.Size = new System.Drawing.Size(75, 35);
            this.playRotatePics_btn.TabIndex = 14;
            this.playRotatePics_btn.Text = "播放";
            this.playRotatePics_btn.UseVisualStyleBackColor = true;
            this.playRotatePics_btn.Click += new System.EventHandler(this.PlayRotatePics_btn_Click);
            // 
            // playPics_picb
            // 
            this.playPics_picb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playPics_picb.Location = new System.Drawing.Point(628, 22);
            this.playPics_picb.Name = "playPics_picb";
            this.playPics_picb.Size = new System.Drawing.Size(400, 400);
            this.playPics_picb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.playPics_picb.TabIndex = 13;
            this.playPics_picb.TabStop = false;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 1033);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Name = "HomeForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playPics_picb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button create_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox originPictureAngle_tbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox playPics_picb;
        private System.Windows.Forms.Button playRotatePics_btn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox finalPictureAngle_tbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox deltaAngle_tbx;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button deletePictures_btn;
    }
}

