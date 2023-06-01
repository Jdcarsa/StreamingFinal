namespace PlataformaStreaming.Vista
{
    partial class Reproducir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reproducir));
            this.panelReproduccion = new System.Windows.Forms.Panel();
            this.reproductorWMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.panelReproduccion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reproductorWMP)).BeginInit();
            this.SuspendLayout();
            // 
            // panelReproduccion
            // 
            this.panelReproduccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(44)))));
            this.panelReproduccion.Controls.Add(this.reproductorWMP);
            this.panelReproduccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReproduccion.Location = new System.Drawing.Point(0, 0);
            this.panelReproduccion.Name = "panelReproduccion";
            this.panelReproduccion.Size = new System.Drawing.Size(800, 450);
            this.panelReproduccion.TabIndex = 1;
            // 
            // reproductorWMP
            // 
            this.reproductorWMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reproductorWMP.Enabled = true;
            this.reproductorWMP.Location = new System.Drawing.Point(0, 0);
            this.reproductorWMP.Name = "reproductorWMP";
            this.reproductorWMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("reproductorWMP.OcxState")));
            this.reproductorWMP.Size = new System.Drawing.Size(800, 450);
            this.reproductorWMP.TabIndex = 0;
            // 
            // Reproducir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelReproduccion);
            this.Name = "Reproducir";
            this.Text = "Reproducir";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelReproduccion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reproductorWMP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelReproduccion;
        private AxWMPLib.AxWindowsMediaPlayer reproductorWMP;
    }
}