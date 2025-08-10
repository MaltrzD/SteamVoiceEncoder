namespace SteamVoiceEncoder
{
    partial class Form1
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
            richInput = new RichTextBox();
            btnSelectFolder = new Button();
            btnStartConvert = new Button();
            label1 = new Label();
            lastTextInfo = new Label();
            lastInfo = new Label();
            openFolderAfterComplete = new CheckBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // richInput
            // 
            richInput.Location = new Point(183, 211);
            richInput.Name = "richInput";
            richInput.Size = new Size(362, 21);
            richInput.TabIndex = 0;
            richInput.Text = "";
            richInput.TextChanged += richInput_TextChanged;
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(551, 211);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(75, 21);
            btnSelectFolder.TabIndex = 1;
            btnSelectFolder.Text = "Обзор";
            btnSelectFolder.UseVisualStyleBackColor = true;
            btnSelectFolder.Click += btnSelectFolder_Click;
            // 
            // btnStartConvert
            // 
            btnStartConvert.Font = new Font("Segoe UI", 13F);
            btnStartConvert.Location = new Point(300, 238);
            btnStartConvert.Name = "btnStartConvert";
            btnStartConvert.Size = new Size(162, 47);
            btnStartConvert.TabIndex = 2;
            btnStartConvert.Text = "Конвертировать";
            btnStartConvert.UseVisualStyleBackColor = true;
            btnStartConvert.Click += btnStartConvert_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(251, 177);
            label1.Name = "label1";
            label1.Size = new Size(249, 25);
            label1.TabIndex = 3;
            label1.Text = "Аудио (mp3, wav, ogg и др.)";
            // 
            // lastTextInfo
            // 
            lastTextInfo.AutoSize = true;
            lastTextInfo.Location = new Point(367, 182);
            lastTextInfo.Name = "lastTextInfo";
            lastTextInfo.Size = new Size(0, 15);
            lastTextInfo.TabIndex = 4;
            // 
            // lastInfo
            // 
            lastInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lastInfo.AutoSize = true;
            lastInfo.Font = new Font("Segoe UI", 15F);
            lastInfo.Location = new Point(12, 9);
            lastInfo.Name = "lastInfo";
            lastInfo.Size = new Size(162, 28);
            lastInfo.TabIndex = 5;
            lastInfo.Text = "aaaaaaaaaaaaaaa";
            lastInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // openFolderAfterComplete
            // 
            openFolderAfterComplete.AutoSize = true;
            openFolderAfterComplete.Checked = true;
            openFolderAfterComplete.CheckState = CheckState.Checked;
            openFolderAfterComplete.Location = new Point(481, 289);
            openFolderAfterComplete.Name = "openFolderAfterComplete";
            openFolderAfterComplete.Size = new Size(15, 14);
            openFolderAfterComplete.TabIndex = 6;
            openFolderAfterComplete.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(291, 288);
            label2.Name = "label2";
            label2.Size = new Size(184, 15);
            label2.TabIndex = 7;
            label2.Text = "Открыть папку при завершении";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(openFolderAfterComplete);
            Controls.Add(lastInfo);
            Controls.Add(lastTextInfo);
            Controls.Add(label1);
            Controls.Add(btnStartConvert);
            Controls.Add(btnSelectFolder);
            Controls.Add(richInput);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Steam Voice Encoder";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richInput;
        private Button btnSelectFolder;
        private Button btnStartConvert;
        private Label label1;
        private Label lastTextInfo;
        private Label lastInfo;
        private CheckBox openFolderAfterComplete;
        private Label label2;
    }
}
