namespace WordformDictionaryTestApp
{
  partial class frmMain
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
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.lstKeywords = new System.Windows.Forms.ListBox();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.cmbKeywords = new System.Windows.Forms.ComboBox();
      this.lstWordforms = new System.Windows.Forms.ListBox();
      this.txtWordform = new System.Windows.Forms.TextBox();
      this.btnDeleteWordform = new System.Windows.Forms.Button();
      this.btnAddWordform = new System.Windows.Forms.Button();
      this.btnDeleteKeyword = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.txtOutput = new System.Windows.Forms.TextBox();
      this.txtInput = new System.Windows.Forms.TextBox();
      this.btnProcess = new System.Windows.Forms.Button();
      this.txtSearchKeywords = new System.Windows.Forms.TextBox();
      this.btnClearSearch = new System.Windows.Forms.Button();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(12, 12);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(176, 238);
      this.tabControl1.TabIndex = 2;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.btnClearSearch);
      this.tabPage1.Controls.Add(this.txtSearchKeywords);
      this.tabPage1.Controls.Add(this.btnSave);
      this.tabPage1.Controls.Add(this.btnLoad);
      this.tabPage1.Controls.Add(this.lstKeywords);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(168, 212);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Словоформы";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.btnDeleteKeyword);
      this.tabPage2.Controls.Add(this.btnAddWordform);
      this.tabPage2.Controls.Add(this.btnDeleteWordform);
      this.tabPage2.Controls.Add(this.txtWordform);
      this.tabPage2.Controls.Add(this.lstWordforms);
      this.tabPage2.Controls.Add(this.cmbKeywords);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(168, 212);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Редактировать";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // lstKeywords
      // 
      this.lstKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lstKeywords.FormattingEnabled = true;
      this.lstKeywords.Location = new System.Drawing.Point(6, 6);
      this.lstKeywords.Name = "lstKeywords";
      this.lstKeywords.Size = new System.Drawing.Size(156, 147);
      this.lstKeywords.TabIndex = 1;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnLoad.Location = new System.Drawing.Point(6, 183);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 23);
      this.btnLoad.TabIndex = 2;
      this.btnLoad.Text = "Загрузить";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(87, 183);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 3;
      this.btnSave.Text = "Сохранить";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // cmbKeywords
      // 
      this.cmbKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cmbKeywords.FormattingEnabled = true;
      this.cmbKeywords.Location = new System.Drawing.Point(6, 6);
      this.cmbKeywords.Name = "cmbKeywords";
      this.cmbKeywords.Size = new System.Drawing.Size(127, 21);
      this.cmbKeywords.TabIndex = 0;
      // 
      // lstWordforms
      // 
      this.lstWordforms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lstWordforms.FormattingEnabled = true;
      this.lstWordforms.Location = new System.Drawing.Point(6, 33);
      this.lstWordforms.Name = "lstWordforms";
      this.lstWordforms.Size = new System.Drawing.Size(127, 147);
      this.lstWordforms.TabIndex = 1;
      // 
      // txtWordform
      // 
      this.txtWordform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtWordform.Location = new System.Drawing.Point(6, 186);
      this.txtWordform.Name = "txtWordform";
      this.txtWordform.Size = new System.Drawing.Size(127, 20);
      this.txtWordform.TabIndex = 2;
      // 
      // btnDeleteWordform
      // 
      this.btnDeleteWordform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDeleteWordform.Location = new System.Drawing.Point(139, 33);
      this.btnDeleteWordform.Name = "btnDeleteWordform";
      this.btnDeleteWordform.Size = new System.Drawing.Size(23, 23);
      this.btnDeleteWordform.TabIndex = 3;
      this.btnDeleteWordform.Text = "X";
      this.btnDeleteWordform.UseVisualStyleBackColor = true;
      // 
      // btnAddWordform
      // 
      this.btnAddWordform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddWordform.Location = new System.Drawing.Point(139, 184);
      this.btnAddWordform.Name = "btnAddWordform";
      this.btnAddWordform.Size = new System.Drawing.Size(23, 23);
      this.btnAddWordform.TabIndex = 4;
      this.btnAddWordform.Text = "+";
      this.btnAddWordform.UseVisualStyleBackColor = true;
      // 
      // btnDeleteKeyword
      // 
      this.btnDeleteKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDeleteKeyword.Location = new System.Drawing.Point(139, 6);
      this.btnDeleteKeyword.Name = "btnDeleteKeyword";
      this.btnDeleteKeyword.Size = new System.Drawing.Size(23, 23);
      this.btnDeleteKeyword.TabIndex = 5;
      this.btnDeleteKeyword.Text = "X";
      this.btnDeleteKeyword.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.Location = new System.Drawing.Point(194, 34);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.txtInput);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.txtOutput);
      this.splitContainer1.Size = new System.Drawing.Size(438, 187);
      this.splitContainer1.SplitterDistance = 92;
      this.splitContainer1.TabIndex = 3;
      // 
      // txtOutput
      // 
      this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtOutput.Location = new System.Drawing.Point(3, 3);
      this.txtOutput.Multiline = true;
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.Size = new System.Drawing.Size(432, 85);
      this.txtOutput.TabIndex = 5;
      // 
      // txtInput
      // 
      this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtInput.Location = new System.Drawing.Point(3, 3);
      this.txtInput.Multiline = true;
      this.txtInput.Name = "txtInput";
      this.txtInput.Size = new System.Drawing.Size(432, 86);
      this.txtInput.TabIndex = 5;
      // 
      // btnProcess
      // 
      this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnProcess.Location = new System.Drawing.Point(557, 227);
      this.btnProcess.Name = "btnProcess";
      this.btnProcess.Size = new System.Drawing.Size(75, 23);
      this.btnProcess.TabIndex = 4;
      this.btnProcess.Text = "Обработать";
      this.btnProcess.UseVisualStyleBackColor = true;
      // 
      // txtSearchKeywords
      // 
      this.txtSearchKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtSearchKeywords.Location = new System.Drawing.Point(6, 157);
      this.txtSearchKeywords.Name = "txtSearchKeywords";
      this.txtSearchKeywords.Size = new System.Drawing.Size(127, 20);
      this.txtSearchKeywords.TabIndex = 4;
      // 
      // btnClearSearch
      // 
      this.btnClearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClearSearch.Location = new System.Drawing.Point(139, 155);
      this.btnClearSearch.Name = "btnClearSearch";
      this.btnClearSearch.Size = new System.Drawing.Size(23, 23);
      this.btnClearSearch.TabIndex = 6;
      this.btnClearSearch.Text = "X";
      this.btnClearSearch.UseVisualStyleBackColor = true;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(644, 262);
      this.Controls.Add(this.btnProcess);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.tabControl1);
      this.Name = "frmMain";
      this.Text = "Form1";
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.ListBox lstKeywords;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Button btnDeleteKeyword;
    private System.Windows.Forms.Button btnAddWordform;
    private System.Windows.Forms.Button btnDeleteWordform;
    private System.Windows.Forms.TextBox txtWordform;
    private System.Windows.Forms.ListBox lstWordforms;
    private System.Windows.Forms.ComboBox cmbKeywords;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox txtInput;
    private System.Windows.Forms.TextBox txtOutput;
    private System.Windows.Forms.Button btnProcess;
    private System.Windows.Forms.Button btnClearSearch;
    private System.Windows.Forms.TextBox txtSearchKeywords;
  }
}

