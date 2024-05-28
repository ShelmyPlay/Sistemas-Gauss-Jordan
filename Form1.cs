using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int m, n = 0;
        public double[,] matriz;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Guardar valores de la matriz.
            if (textBox1.Text != "")
            {
                m = int.Parse(textBox1.Text);
                n = m + 1;
            }
        }

        //Verificar solo numeros.
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<double> valores = new List<double>();
            bool error = false;

            //Guardar Matriz
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    System.Windows.Forms.TextBox textBox = this.Controls.Find("txtMat" + i + "_" + j, true).FirstOrDefault() as System.Windows.Forms.TextBox;
                    if (textBox != null)
                    {
                        if (double.TryParse(textBox.Text, out double value))
                        {
                            valores.Add(value);
                            matriz[i, j] = value;
                        }
                        else
                        {
                            MessageBox.Show($"Valor inválido en {i + 1}, {j + 1}. Por favor, introduce un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            //Resolución Gauss-Jordan
            for (int i = 0; i < m; i++)
            {
                double a = matriz[i, i];
                //Deteccion de errores
                if (a == 0)
                {
                    MessageBox.Show("El sistema no tiene solución.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error = true;
                    break;
                }
                if (a != 1)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matriz[i, j] /= a;
                    }
                
                }
                for (int j = 0; j < m; j++)
                {
                    double b = matriz[j, i];
                    if (i != j && b != 0)
                    {
                        for (int k = 0; k < n; k++)
                        {
                            matriz[j, k] -= (b * matriz[i, k]);
                        }
                    }
                }
            }

            //Imprimir Resultado
            if (error)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        System.Windows.Forms.TextBox textBox = this.Controls.Find("txtMat" + i + "_" + j, true).FirstOrDefault() as System.Windows.Forms.TextBox;
                        if (textBox != null)
                        {
                            textBox.Text = "";
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < m; i++)
                {
                    Label newLabel = new Label();
                    newLabel.Name = "lblRes" + i;
                    newLabel.Text = "x_" + (i + 1) + " = " + matriz[i, m].ToString("F4");
                    newLabel.Location = new System.Drawing.Point(12, 219 + (26 * i));
                    this.Controls.Add(newLabel);
                }
                button2.Enabled = false;
                button4.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Limpiar casillas y textbox
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    System.Windows.Forms.TextBox textBox = this.Controls.Find("txtMat" + i + "_" + j, true).FirstOrDefault() as System.Windows.Forms.TextBox;
                    if (textBox != null)
                    {
                        this.Controls.Remove(textBox);
                        textBox.Dispose();
                    }
                }
                Label label = this.Controls.Find("lblRes" + i, true).FirstOrDefault() as Label;
                if (label != null)
                {
                    this.Controls.Remove(label);
                    label.Dispose();
                }
            }

            // Reiniciar la matriz y el contador
            matriz = null;
            m = 0;
            n = 0;
            textBox1.Clear();
            button1.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = false;
            textBox1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m; i++)
            {
                Label label = this.Controls.Find("lblRes" + i, true).FirstOrDefault() as Label;
                if (label != null)
                {
                    this.Controls.Remove(label);
                    label.Dispose();
                }

                for (int j = 0; j < n; j++)
                {
                    System.Windows.Forms.TextBox textBox = this.Controls.Find("txtMat" + i + "_" + j, true).FirstOrDefault() as System.Windows.Forms.TextBox;
                    if (textBox != null)
                    {
                        textBox.Text = "";
                    }
                }
            }
            button2.Enabled = true;
            button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Generar casillas para ingresar valores
            if (m == 0)
            {
                MessageBox.Show("Ingrese las dimensiones de la matriz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                matriz = new double[m, n];
                for (int i = 0; i< m; i++)
                {
                    for(int j = 0; j < n; j++)
                    {
                        System.Windows.Forms.TextBox newTextBox = new System.Windows.Forms.TextBox();
                        newTextBox.Name = "txtMat" + i + "_" + j;
                        newTextBox.Location = new System.Drawing.Point(92 + (26 * j), 34 + (26 * i)); // Ubicación
                        newTextBox.Size = new System.Drawing.Size(20, 20);
                        this.Controls.Add(newTextBox);
                    }
                }
                button1.Enabled = false;
                button2.Enabled = true;
                textBox1.Enabled = false;
            }
        }
    }
}
