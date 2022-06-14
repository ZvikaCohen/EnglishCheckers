
namespace B22_Ex05_Zvika_208494245_Omri_315873471
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
            this.SixOnSixButton = new System.Windows.Forms.RadioButton();
            this.EightOnEightButton = new System.Windows.Forms.RadioButton();
            this.TenOnTenButton = new System.Windows.Forms.RadioButton();
            this.BoardSizeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PlayerTwoCheckBox = new System.Windows.Forms.CheckBox();
            this.PlayerOneNameTextBox = new System.Windows.Forms.TextBox();
            this.PlayerTwoNameTextBox = new System.Windows.Forms.TextBox();
            this.PlayersLabel = new System.Windows.Forms.Label();
            this.PlayerOneNameLabel = new System.Windows.Forms.Label();
            this.DoneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SixOnSixButton
            // 
            this.SixOnSixButton.AccessibleName = "";
            this.SixOnSixButton.AutoSize = true;
            this.SixOnSixButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SixOnSixButton.Location = new System.Drawing.Point(29, 75);
            this.SixOnSixButton.Name = "SixOnSixButton";
            this.SixOnSixButton.Size = new System.Drawing.Size(69, 29);
            this.SixOnSixButton.TabIndex = 0;
            this.SixOnSixButton.TabStop = true;
            this.SixOnSixButton.Text = "6x6";
            this.SixOnSixButton.UseVisualStyleBackColor = true;
            this.SixOnSixButton.CheckedChanged += new System.EventHandler(this.SixOnSixButton_CheckedChanged);
            // 
            // EightOnEightButton
            // 
            this.EightOnEightButton.AutoSize = true;
            this.EightOnEightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.EightOnEightButton.Location = new System.Drawing.Point(120, 75);
            this.EightOnEightButton.Name = "EightOnEightButton";
            this.EightOnEightButton.Size = new System.Drawing.Size(69, 29);
            this.EightOnEightButton.TabIndex = 1;
            this.EightOnEightButton.TabStop = true;
            this.EightOnEightButton.Text = "8x8";
            this.EightOnEightButton.UseVisualStyleBackColor = true;
            this.EightOnEightButton.CheckedChanged += new System.EventHandler(this.EightOnEightButton_CheckedChanged);
            // 
            // TenOnTenButton
            // 
            this.TenOnTenButton.AutoSize = true;
            this.TenOnTenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.TenOnTenButton.Location = new System.Drawing.Point(204, 75);
            this.TenOnTenButton.Name = "TenOnTenButton";
            this.TenOnTenButton.Size = new System.Drawing.Size(91, 29);
            this.TenOnTenButton.TabIndex = 2;
            this.TenOnTenButton.TabStop = true;
            this.TenOnTenButton.Text = "10x10";
            this.TenOnTenButton.UseVisualStyleBackColor = true;
            this.TenOnTenButton.CheckedChanged += new System.EventHandler(this.TenOnTenButton_CheckedChanged);
            // 
            // BoardSizeLabel
            // 
            this.BoardSizeLabel.AutoSize = true;
            this.BoardSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.BoardSizeLabel.Location = new System.Drawing.Point(24, 32);
            this.BoardSizeLabel.Name = "BoardSizeLabel";
            this.BoardSizeLabel.Size = new System.Drawing.Size(114, 25);
            this.BoardSizeLabel.TabIndex = 3;
            this.BoardSizeLabel.Text = "Board Size:";
            this.BoardSizeLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 5;
            // 
            // PlayerTwoCheckBox
            // 
            this.PlayerTwoCheckBox.AutoSize = true;
            this.PlayerTwoCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayerTwoCheckBox.Location = new System.Drawing.Point(43, 205);
            this.PlayerTwoCheckBox.Name = "PlayerTwoCheckBox";
            this.PlayerTwoCheckBox.Size = new System.Drawing.Size(115, 29);
            this.PlayerTwoCheckBox.TabIndex = 6;
            this.PlayerTwoCheckBox.Text = "Player 2:";
            this.PlayerTwoCheckBox.UseVisualStyleBackColor = true;
            this.PlayerTwoCheckBox.Click += new System.EventHandler(this.PlayerTwoCheckBox_Clicked);
            // 
            // PlayerOneNameTextBox
            // 
            this.PlayerOneNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayerOneNameTextBox.Location = new System.Drawing.Point(181, 165);
            this.PlayerOneNameTextBox.Name = "PlayerOneNameTextBox";
            this.PlayerOneNameTextBox.Size = new System.Drawing.Size(133, 30);
            this.PlayerOneNameTextBox.TabIndex = 7;
            // 
            // PlayerTwoNameTextBox
            // 
            this.PlayerTwoNameTextBox.Enabled = false;
            this.PlayerTwoNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayerTwoNameTextBox.Location = new System.Drawing.Point(181, 205);
            this.PlayerTwoNameTextBox.Name = "PlayerTwoNameTextBox";
            this.PlayerTwoNameTextBox.Size = new System.Drawing.Size(133, 30);
            this.PlayerTwoNameTextBox.TabIndex = 8;
            this.PlayerTwoNameTextBox.Text = "[Computer]";
            this.PlayerTwoNameTextBox.TextChanged += new System.EventHandler(this.PlayerTwoNameTextBox_TextChanged);
            // 
            // PlayersLabel
            // 
            this.PlayersLabel.AutoSize = true;
            this.PlayersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayersLabel.Location = new System.Drawing.Point(24, 116);
            this.PlayersLabel.Name = "PlayersLabel";
            this.PlayersLabel.Size = new System.Drawing.Size(83, 25);
            this.PlayersLabel.TabIndex = 9;
            this.PlayersLabel.Text = "Players:";
            this.PlayersLabel.Click += new System.EventHandler(this.PlayersLabel_Click_1);
            // 
            // PlayerOneNameLabel
            // 
            this.PlayerOneNameLabel.AutoSize = true;
            this.PlayerOneNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PlayerOneNameLabel.Location = new System.Drawing.Point(38, 160);
            this.PlayerOneNameLabel.Name = "PlayerOneNameLabel";
            this.PlayerOneNameLabel.Size = new System.Drawing.Size(89, 25);
            this.PlayerOneNameLabel.TabIndex = 10;
            this.PlayerOneNameLabel.Text = "Player 1:";
            this.PlayerOneNameLabel.Click += new System.EventHandler(this.PlayerOneNameLabel_Click);
            // 
            // DoneButton
            // 
            this.DoneButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DoneButton.Location = new System.Drawing.Point(181, 270);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(133, 38);
            this.DoneButton.TabIndex = 11;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 320);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.PlayerOneNameLabel);
            this.Controls.Add(this.PlayersLabel);
            this.Controls.Add(this.PlayerTwoNameTextBox);
            this.Controls.Add(this.PlayerOneNameTextBox);
            this.Controls.Add(this.PlayerTwoCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BoardSizeLabel);
            this.Controls.Add(this.TenOnTenButton);
            this.Controls.Add(this.EightOnEightButton);
            this.Controls.Add(this.SixOnSixButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton SixOnSixButton;
        private System.Windows.Forms.RadioButton EightOnEightButton;
        private System.Windows.Forms.RadioButton TenOnTenButton;
        private System.Windows.Forms.Label BoardSizeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox PlayerTwoCheckBox;
        private System.Windows.Forms.TextBox PlayerOneNameTextBox;
        private System.Windows.Forms.TextBox PlayerTwoNameTextBox;
        private System.Windows.Forms.Label PlayersLabel;
        private System.Windows.Forms.Label PlayerOneNameLabel;
        private System.Windows.Forms.Button DoneButton;
    }
}