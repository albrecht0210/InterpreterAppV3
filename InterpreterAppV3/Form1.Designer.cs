namespace InterpreterAppV3
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
            code_input = new RichTextBox();
            run_btn = new Button();
            SuspendLayout();
            // 
            // code_input
            // 
            code_input.Location = new Point(12, 12);
            code_input.Name = "code_input";
            code_input.Size = new Size(360, 308);
            code_input.TabIndex = 0;
            code_input.Text = "";
            // 
            // run_btn
            // 
            run_btn.Location = new Point(297, 326);
            run_btn.Name = "run_btn";
            run_btn.Size = new Size(75, 23);
            run_btn.TabIndex = 1;
            run_btn.Text = "Run";
            run_btn.UseVisualStyleBackColor = true;
            run_btn.Click += run_btn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 361);
            Controls.Add(run_btn);
            Controls.Add(code_input);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox code_input;
        private Button run_btn;
    }
}