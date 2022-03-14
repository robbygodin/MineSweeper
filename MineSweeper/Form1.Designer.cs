
namespace MineSweeper
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.exLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerLabel = new System.Windows.Forms.Label();
            this.flagsLabel = new System.Windows.Forms.Label();
            this.flagLabelLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lineLabel = new System.Windows.Forms.Label();
            this.showBombs = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // exLabel
            // 
            this.exLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.exLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exLabel.Location = new System.Drawing.Point(322, 380);
            this.exLabel.Margin = new System.Windows.Forms.Padding(0);
            this.exLabel.Name = "exLabel";
            this.exLabel.Size = new System.Drawing.Size(50, 50);
            this.exLabel.TabIndex = 0;
            this.exLabel.Text = "8";
            this.exLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exLabel.Visible = false;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerLabel
            // 
            this.timerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timerLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(152)))), ((int)(((byte)(218)))));
            this.timerLabel.Location = new System.Drawing.Point(825, 9);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(163, 78);
            this.timerLabel.TabIndex = 1;
            this.timerLabel.Text = "00:00";
            this.timerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagsLabel
            // 
            this.flagsLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(152)))), ((int)(((byte)(218)))));
            this.flagsLabel.Location = new System.Drawing.Point(80, 16);
            this.flagsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.flagsLabel.Name = "flagsLabel";
            this.flagsLabel.Size = new System.Drawing.Size(86, 65);
            this.flagsLabel.TabIndex = 2;
            this.flagsLabel.Text = "40";
            this.flagsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flagLabelLabel
            // 
            this.flagLabelLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagLabelLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(152)))), ((int)(((byte)(218)))));
            this.flagLabelLabel.Location = new System.Drawing.Point(20, 16);
            this.flagLabelLabel.Margin = new System.Windows.Forms.Padding(0);
            this.flagLabelLabel.Name = "flagLabelLabel";
            this.flagLabelLabel.Size = new System.Drawing.Size(59, 65);
            this.flagLabelLabel.TabIndex = 3;
            this.flagLabelLabel.Text = "fdasfds";
            this.flagLabelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(152)))), ((int)(((byte)(218)))));
            this.titleLabel.Location = new System.Drawing.Point(273, 9);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(455, 78);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "Minesweeper";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(152)))), ((int)(((byte)(218)))));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 78);
            this.label1.TabIndex = 5;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lineLabel
            // 
            this.lineLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lineLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineLabel.Location = new System.Drawing.Point(-5, 99);
            this.lineLabel.Margin = new System.Windows.Forms.Padding(0);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(1011, 50);
            this.lineLabel.TabIndex = 6;
            this.lineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // showBombs
            // 
            this.showBombs.WorkerReportsProgress = true;
            this.showBombs.WorkerSupportsCancellation = true;
            this.showBombs.DoWork += new System.ComponentModel.DoWorkEventHandler(this.showBombs_DoWork);
            this.showBombs.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.showBombs_ProgressChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(216)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.flagLabelLabel);
            this.Controls.Add(this.flagsLabel);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.exLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lineLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label exLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Label flagsLabel;
        private System.Windows.Forms.Label flagLabelLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lineLabel;
        private System.ComponentModel.BackgroundWorker showBombs;
    }
}

