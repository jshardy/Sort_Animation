/*
Author/Copyright:	Joshua Lee Hardy
E-mail:				Joshua.Hardy@oit.edu
Date:				4/24/2018
Originated:			Oregon Tech - Data Structures CST211

    Sort_Daddy
	Be free :)~. Licensed under GPL V2

This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.If not, see<http://www.gnu.org/licenses/>.
*/

/* Purpose is to be able to cut and paste C/C++ sort code
 * into program with minor modifications to give you visual
 * feedback of how your sort works.
 * 
 * Use AnimationArray as you would a normal int array.
 * -Have not tested if gapped array works....
 * -this does animation so it must find the other integer
 *  being swapped. Some sorts may not work properly.
 * -Use array sizes of 10 for best results
 *  Array sizes larger than 10 at your own risk
 *  
 * -Help me fix it.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Sort_Daddy
{
    public class AnimationArray
    {
        private Control m_parent;
        private int m_size;
        private int m_animation_interval = 5;
        private int m_animation_step = 5;
        private PictureBox[] m_array;
        private Label m_label_array = null;
        private Control m_current_control_animation;
        private int m_new_x;
        private bool m_paused = false;

        private Button m_pause_button = null;

        private Timer timer_animation;

        //x and y location are static..... must modify code..
        //maybe someone else is board enough to fix it for me?
        public AnimationArray(Form parent, int size = 10, int x = 0, int y = 0)
        {
            m_parent = parent;
            m_size = size;

            m_array = new PictureBox[m_size];
            for(int i = 0; i < m_size; i++)
            {
                m_array[i] = new PictureBox();
                m_array[i].Name = i.ToString();

                m_array[i].Width = 40;
                m_array[i].Height = 30 * (i + 1);

                m_array[i].Left = ((i + 1) * (m_array[i].Width + 5)) + x;

                if (i == 0)
                    m_array[0].Top = 300 + y;
                else
                    m_array[i].Top = (330 - ((i + 1) * 30)) + y;

                m_array[i].Paint += new System.Windows.Forms.PaintEventHandler(this.picturebox_draw);

                m_array[i].BackColor = Color.Aquamarine;
                m_parent.Controls.Add(m_array[i]);
            }
            ResetColors();
            timer_animation = new Timer();
            timer_animation.Enabled = false;
            timer_animation.Interval = m_animation_interval;
            timer_animation.Tick += new EventHandler(this.timer_animation_tick);
        }
        public void ResetColors()
        {
            for(int i = 0; i < m_size; i++)
            {
                switch(i)
                {
                    case 0:
                        m_array[i].BackColor = Color.Aqua;
                        break;
                    case 1:
                        m_array[i].BackColor = Color.Peru;
                        break;
                    case 2:
                        m_array[i].BackColor = Color.Plum;
                        break;
                    case 3:
                        m_array[i].BackColor = Color.Salmon;
                        break;
                    case 4:
                        m_array[i].BackColor = Color.Coral;
                        break;
                    case 5:
                        m_array[i].BackColor = Color.CornflowerBlue;
                        break;
                    case 6:
                        m_array[i].BackColor = Color.DarkGreen;
                        break;
                    case 7:
                        m_array[i].BackColor = Color.LimeGreen;
                        break;
                    case 8:
                        m_array[i].BackColor = Color.YellowGreen;
                        break;
                    case 9:
                        m_array[i].BackColor = Color.Violet;
                        break;
                    case 10:
                        m_array[i].BackColor = Color.Tomato;
                        break;
                }
            }
        }
        private void picturebox_draw(object sender, PaintEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            String picture_label = p.Name;

            using (Font myFont = new Font("Arial", 20))
            {
                e.Graphics.DrawString(picture_label, myFont, Brushes.Black, new Point(3, p.Height - 30));
            }
        }

        private void move(Control control1, int destination_x)
        {
            m_current_control_animation = control1;
            m_new_x = destination_x;

            timer_animation.Enabled = true;
        }

        private void move(Control control1, Control control2)
        {
            //move the controls one by one.
            //no complicated stuff.
            control2.Visible = false;
            int old_x_val = control1.Left;
            move(control1, control2.Left);

            while (timer_animation.Enabled)
                Application.DoEvents();

            control2.Visible = true;
            move(control2, old_x_val);

            while (timer_animation.Enabled)
                Application.DoEvents();

            //physically swap the pictureboxes in the array
            //so that we keep track of where they are going.
            int temp1 = -1;
            int temp2 = -1;

            for (int i = 0; i < m_size; i++)
            {
                if (control1 == m_array[i])
                    temp1 = i;
                if (control2 == m_array[i])
                    temp2 = i;

                if (temp1 > -1 && temp2 > -1)
                    break;
            }

            if (temp2 > -1 && temp1 > -1)
            {
                Control temp = m_array[temp1];
                m_array[temp1] = m_array[temp2];
                m_array[temp2] = (PictureBox)temp;
            }
            else
                MessageBox.Show("Error swapping array values.\nYou broke it!!!");

            //do not return until move is done
            while (timer_animation.Enabled)
                Application.DoEvents();

            update_label();

            //do stepping
            bool past_value = m_paused;
            while (m_paused)
                Application.DoEvents();

            if(past_value)
                m_paused = true;
        }

        private void continue_animation()
        {
            if (Math.Abs(m_current_control_animation.Left - m_new_x) < 5) //end
            {
                m_current_control_animation.Left = m_new_x;
                m_current_control_animation = null;
                timer_animation.Enabled = false;
            }
            else if (m_current_control_animation.Left > m_new_x)
            {
                //subtract going left
                m_current_control_animation.Left -= m_animation_step;
            }
            else if (m_current_control_animation.Left < m_new_x)
            {
                //add going right
                m_current_control_animation.Left += m_animation_step;
            }
        }

        private void timer_animation_tick(object sender, EventArgs e)
        {
            continue_animation();
        }

        private void button_step_click(object sender, EventArgs e)
        {
            m_paused = false;
        }

        private void update_label()
        {
            if (m_label_array != null)
            {
                m_label_array.Text = "Array: ";
                for (int i = 0; i < m_array.Length; i++)
                {
                    if (i < m_array.Length)
                        m_label_array.Text += this[i].ToString() + ", ";
                    else if (i == m_array.Length - 1)
                        m_label_array.Text += this[i].ToString();
                }
            }
        }
        public void Randomize()
        {
            Random r = new Random();
            //do randomize at double speed
            int anim_step_previous = m_animation_step;
            m_animation_step = 15;

            for(int i = 0; i < m_size; i++)
            {
                int random_value = r.Next() % m_size;
                this[i] = random_value;
            }
            m_animation_step = anim_step_previous;
        }
        public int this[int index]
        {
            get
            {
                return Convert.ToInt32(m_array[index].Name);
            }
            set
            {
                if (index >= 0 && index <= m_size)
                {
                    Control control1 = m_array[index];
                    Control control2 = null;

                    //find second control to change value too.
                    for (int i = 0; i < m_size; i++)
                        if (value == Convert.ToInt32(m_array[i].Name))
                            control2 = m_array[i];

                    move(control1, control2);
                }
                else
                    throw new Exception("Error out of bounds access on array index operator");
            }
        }

        public void swap(int x, int y)
        {
            Control control1 = null;
            Control control2 = null;

            //find the controls
            for (int i = 0; i < m_size; i++)
            {
                if (x == Convert.ToInt32(m_array[i].Name))
                    control1 = m_array[i];
                if (y == Convert.ToInt32(m_array[i].Name))
                    control2 = m_array[i];

                if (control1 != null && control2 != null)
                    break;
            }
            //move them
            move(control1, control2);
        }

        public int animation_step
        {
            get
            {
                return m_animation_step;
            }
            set
            {
                m_animation_step = value;
            }
        }

        public int Length
        {
            get
            {
                return m_size;
            }
            //dont set.. make a new class.
        }

        public Button StepButton
        {
            set
            {
                m_pause_button = value;

                if (m_pause_button != null)
                {
                    m_pause_button.Click += new EventHandler(button_step_click);
                    m_paused = true;
                }
                else
                    m_paused = false;
            }
        }
        public Label Label
        {
            //get { return label_array; }
            set { m_label_array = value; }
        }
    }
}
