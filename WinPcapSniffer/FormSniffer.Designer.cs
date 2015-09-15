namespace WinPcapSniffer
{
    partial class FormSniffer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.btnList = new System.Windows.Forms.Button();
            this.cmbDevice = new System.Windows.Forms.ComboBox();
            this.txtFilters = new System.Windows.Forms.TextBox();
            this.lblFilters = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.lblListening = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.btnReSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 546);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.Location = new System.Drawing.Point(13, 40);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(812, 500);
            this.rtxtOutput.TabIndex = 1;
            this.rtxtOutput.Text = "";
            this.rtxtOutput.TextChanged += new System.EventHandler(this.rtxtOutput_TextChanged);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(750, 546);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 23);
            this.btnList.TabIndex = 2;
            this.btnList.Text = "List Devices";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // cmbDevice
            // 
            this.cmbDevice.FormattingEnabled = true;
            this.cmbDevice.Location = new System.Drawing.Point(154, 548);
            this.cmbDevice.Name = "cmbDevice";
            this.cmbDevice.Size = new System.Drawing.Size(282, 21);
            this.cmbDevice.TabIndex = 4;
            // 
            // txtFilters
            // 
            this.txtFilters.Location = new System.Drawing.Point(153, 577);
            this.txtFilters.Name = "txtFilters";
            this.txtFilters.Size = new System.Drawing.Size(118, 20);
            this.txtFilters.TabIndex = 5;
            // 
            // lblFilters
            // 
            this.lblFilters.AutoSize = true;
            this.lblFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilters.Location = new System.Drawing.Point(92, 578);
            this.lblFilters.Name = "lblFilters";
            this.lblFilters.Size = new System.Drawing.Size(55, 16);
            this.lblFilters.TabIndex = 6;
            this.lblFilters.Text = "Filters:";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(13, 575);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(750, 575);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 8;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // lblListening
            // 
            this.lblListening.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListening.Location = new System.Drawing.Point(13, 13);
            this.lblListening.Name = "lblListening";
            this.lblListening.Size = new System.Drawing.Size(812, 16);
            this.lblListening.TabIndex = 9;
            this.lblListening.Text = "Not Listening";
            this.lblListening.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevice.Location = new System.Drawing.Point(92, 549);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(61, 16);
            this.lblDevice.TabIndex = 10;
            this.lblDevice.Text = "Device:";
            // 
            // btnReSend
            // 
            this.btnReSend.Location = new System.Drawing.Point(467, 575);
            this.btnReSend.Name = "btnReSend";
            this.btnReSend.Size = new System.Drawing.Size(75, 23);
            this.btnReSend.TabIndex = 11;
            this.btnReSend.Text = "Resend";
            this.btnReSend.UseVisualStyleBackColor = true;
            this.btnReSend.Click += new System.EventHandler(this.btnReSend_Click);
            // 
            // FormSniffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 604);
            this.Controls.Add(this.btnReSend);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.lblListening);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblFilters);
            this.Controls.Add(this.txtFilters);
            this.Controls.Add(this.cmbDevice);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.rtxtOutput);
            this.Controls.Add(this.btnStart);
            this.Name = "FormSniffer";
            this.Text = "Sniffer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.ComboBox cmbDevice;
        private System.Windows.Forms.TextBox txtFilters;
        private System.Windows.Forms.Label lblFilters;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Label lblListening;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Button btnReSend;
    }
}

