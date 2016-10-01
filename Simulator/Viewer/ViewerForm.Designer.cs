namespace Viewer
{
    partial class ViewerForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
			this.draw = new System.Windows.Forms.PictureBox();
			this.startButton = new System.Windows.Forms.Button();
			this.manualCheckBox = new System.Windows.Forms.CheckBox();
			this.nextFrameButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.OptionPath = new System.Windows.Forms.TextBox();
			this.OptionLoadingButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.PlayFrame = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.draw)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PlayFrame)).BeginInit();
			this.SuspendLayout();
			// 
			// draw
			// 
			this.draw.BackColor = System.Drawing.Color.White;
			this.draw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.draw.Location = new System.Drawing.Point(12, 93);
			this.draw.Name = "draw";
			this.draw.Size = new System.Drawing.Size(1462, 756);
			this.draw.TabIndex = 0;
			this.draw.TabStop = false;
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(12, 12);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(280, 75);
			this.startButton.TabIndex = 1;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// manualCheckBox
			// 
			this.manualCheckBox.AutoSize = true;
			this.manualCheckBox.Location = new System.Drawing.Point(298, 16);
			this.manualCheckBox.Name = "manualCheckBox";
			this.manualCheckBox.Size = new System.Drawing.Size(94, 19);
			this.manualCheckBox.TabIndex = 2;
			this.manualCheckBox.Text = "수동 재생";
			this.manualCheckBox.UseVisualStyleBackColor = true;
			this.manualCheckBox.CheckedChanged += new System.EventHandler(this.manualCheckBox_CheckedChanged);
			// 
			// nextFrameButton
			// 
			this.nextFrameButton.Location = new System.Drawing.Point(298, 50);
			this.nextFrameButton.Name = "nextFrameButton";
			this.nextFrameButton.Size = new System.Drawing.Size(180, 37);
			this.nextFrameButton.TabIndex = 3;
			this.nextFrameButton.Text = "▶ 다음 프레임";
			this.nextFrameButton.UseVisualStyleBackColor = true;
			this.nextFrameButton.Click += new System.EventHandler(this.nextFrameButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(490, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 15);
			this.label1.TabIndex = 4;
			this.label1.Text = "옵션 파일";
			// 
			// OptionPath
			// 
			this.OptionPath.Location = new System.Drawing.Point(566, 13);
			this.OptionPath.Name = "OptionPath";
			this.OptionPath.ReadOnly = true;
			this.OptionPath.Size = new System.Drawing.Size(819, 25);
			this.OptionPath.TabIndex = 5;
			// 
			// OptionLoadingButton
			// 
			this.OptionLoadingButton.Location = new System.Drawing.Point(1391, 12);
			this.OptionLoadingButton.Name = "OptionLoadingButton";
			this.OptionLoadingButton.Size = new System.Drawing.Size(83, 25);
			this.OptionLoadingButton.TabIndex = 6;
			this.OptionLoadingButton.Text = "...";
			this.OptionLoadingButton.UseVisualStyleBackColor = true;
			this.OptionLoadingButton.Click += new System.EventHandler(this.OptionLoadingButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(490, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 15);
			this.label2.TabIndex = 7;
			this.label2.Text = "초당 재생 프레임";
			// 
			// PlayFrame
			// 
			this.PlayFrame.Location = new System.Drawing.Point(619, 58);
			this.PlayFrame.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.PlayFrame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PlayFrame.Name = "PlayFrame";
			this.PlayFrame.Size = new System.Drawing.Size(73, 25);
			this.PlayFrame.TabIndex = 8;
			this.PlayFrame.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// ViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1486, 863);
			this.Controls.Add(this.PlayFrame);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.OptionLoadingButton);
			this.Controls.Add(this.OptionPath);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nextFrameButton);
			this.Controls.Add(this.manualCheckBox);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.draw);
			this.Name = "ViewerForm";
			this.Text = "Viewer";
			((System.ComponentModel.ISupportInitialize)(this.draw)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PlayFrame)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.PictureBox draw;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.CheckBox manualCheckBox;
		private System.Windows.Forms.Button nextFrameButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox OptionPath;
		private System.Windows.Forms.Button OptionLoadingButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown PlayFrame;
	}
}

