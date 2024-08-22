namespace DP_manager.Components
{
    partial class HistoryForm
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
            this.tlp_stock = new System.Windows.Forms.TableLayoutPanel();
            this.pageControl1 = new DP_manager.Components.PageControl();
            this.tlp_stock.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_stock
            // 
            this.tlp_stock.ColumnCount = 2;
            this.tlp_stock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_stock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_stock.Controls.Add(this.pageControl1, 0, 1);
            this.tlp_stock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_stock.Location = new System.Drawing.Point(0, 0);
            this.tlp_stock.Name = "tlp_stock";
            this.tlp_stock.RowCount = 2;
            this.tlp_stock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_stock.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_stock.Size = new System.Drawing.Size(800, 450);
            this.tlp_stock.TabIndex = 2;
            // 
            // pageControl1
            // 
            this.pageControl1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pageControl1.AutoSize = true;
            this.pageControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pageControl1.Location = new System.Drawing.Point(301, 406);
            this.pageControl1.Name = "pageControl1";
            this.pageControl1.Page = 1;
            this.pageControl1.PageCount = 1;
            this.pageControl1.Size = new System.Drawing.Size(197, 41);
            this.pageControl1.TabIndex = 0;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_stock);
            this.Name = "HistoryForm";
            this.Text = "HistoryForm";
            this.tlp_stock.ResumeLayout(false);
            this.tlp_stock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_stock;
        private PageControl pageControl1;
    }
}