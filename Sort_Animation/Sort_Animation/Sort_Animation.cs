using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using sort = Sort_Daddy.AnimationArray;

namespace Sort_Daddy
{
    public partial class Sort_Animation : Form
    {
        AnimationArray array;
        public Sort_Animation()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            array = new AnimationArray(this, 10);
            //attach a debug label
            array.Label = label_array;
            trackbar_animation_speed.Value = array.animation_step;
            label2.Text = "Anim Speed: " + array.animation_step.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            array.Randomize();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int swap = 0;

            for (int loop = 0; loop < array.Length; loop++)
            {
                for (int count = 0; count < (array.Length - 1) - loop; count++)
                {
                    if (array[count] > array[count + 1])
                    {
                        swap = array[count + 1];
                        array[count + 1] = array[count];
                        array[count] = swap;
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnimationArray int_sort_data = array;
            int n = array.Length;

            int min;
            for (int i = 0; i < n; i++)
            {
                min = i;
                for (int j = i; j < n - 1; j++)
                {
                    if (int_sort_data[j + 1] < int_sort_data[min])
                    {
                        min = j + 1;
                    }
                }
                array.swap(int_sort_data[i], int_sort_data[min]);
            }


        }
        private void swap(int i, int j)
        {
            int temp = i;
            i = j;
            j = temp;
        }

        private void checkbox_step_through_animation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox_step_through_animation.Checked)
                array.StepButton = button_step;
            else
                array.StepButton = null;
        }

        private void button_step_Click(object sender, EventArgs e)
        {

        }

        private void trackbar_animation_speed_ValueChanged(object sender, EventArgs e)
        {
            array.animation_step = trackbar_animation_speed.Value;
            label2.Text = "Anim Speed: " + array.animation_step.ToString();
        }
    }
}
