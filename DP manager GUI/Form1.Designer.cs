using DP_manager.Components;

namespace DP_manager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tlp_stock = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tlp_archive = new System.Windows.Forms.TableLayoutPanel();
            this.pageControl2 = new DP_manager.Components.PageControl();
            this.pageControl1 = new DP_manager.Components.PageControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tlp_stock.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tlp_archive.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1031, 581);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tlp_stock);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1023, 552);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Stock";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tlp_stock
            // 
            this.tlp_stock.ColumnCount = 2;
            this.tlp_stock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_stock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_stock.Controls.Add(this.pageControl1, 0, 1);
            this.tlp_stock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_stock.Location = new System.Drawing.Point(3, 3);
            this.tlp_stock.Name = "tlp_stock";
            this.tlp_stock.RowCount = 2;
            this.tlp_stock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_stock.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_stock.Size = new System.Drawing.Size(1017, 546);
            this.tlp_stock.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tlp_archive);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1023, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Archive";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tlp_archive
            // 
            this.tlp_archive.ColumnCount = 2;
            this.tlp_archive.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_archive.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_archive.Controls.Add(this.pageControl2, 0, 1);
            this.tlp_archive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_archive.Location = new System.Drawing.Point(3, 3);
            this.tlp_archive.Name = "tlp_archive";
            this.tlp_archive.RowCount = 2;
            this.tlp_archive.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_archive.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_archive.Size = new System.Drawing.Size(1017, 546);
            this.tlp_archive.TabIndex = 2;
            // 
            // pageControl2
            // 
            this.pageControl2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pageControl2.AutoSize = true;
            this.pageControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pageControl2.Location = new System.Drawing.Point(410, 502);
            this.pageControl2.Name = "pageControl2";
            this.pageControl2.Page = 1;
            this.pageControl2.PageCount = 1;
            this.pageControl2.Size = new System.Drawing.Size(197, 41);
            this.pageControl2.TabIndex = 0;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pageControl1.AutoSize = true;
            this.pageControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pageControl1.Location = new System.Drawing.Point(410, 502);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Page = 1;
            this.pageControl1.PageCount = 1;
            this.pageControl1.Size = new System.Drawing.Size(197, 41);
            this.pageControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 581);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Denis-Plants stock manager";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tlp_stock.ResumeLayout(false);
            this.tlp_stock.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tlp_archive.ResumeLayout(false);
            this.tlp_archive.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tlp_stock;
        private System.Windows.Forms.TableLayoutPanel tlp_archive;
        private PageControl pageControl2;
        private PageControl pageControl1;
    }
}

