﻿namespace AgCubio
{
    partial class ConnectForm
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
            this.lblAddress = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(23, 16);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Address";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(15, 97);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(218, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(74, 13);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(159, 20);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbox_keyDown);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(74, 39);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(56, 20);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "11000";
            this.txtPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbox_keyDown);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(42, 42);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "Port";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 127);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 5;
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 150);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConnectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect to AgCubio Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblStatus;
    }
}