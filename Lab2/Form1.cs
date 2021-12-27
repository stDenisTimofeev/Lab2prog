using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab2
{
    public partial class Form1 : Form
    {

        //string name;
        IGraphicFactory factory = new DefaultFactory();
        List<GraphObject> elements = new List<GraphObject>();
        int id = -1;
        List<Queue<QueueObjects>> arrayOfQueue = new List<Queue<QueueObjects>>(); 
        Queue<QueueObjects> queues = new Queue<QueueObjects>();
        Dictionary<GraphObject, Queue<QueueObjects>> mappingIdQueue = new Dictionary<GraphObject, Queue<QueueObjects>>();

        public Form1()
        {
            
            /*Random rand = new Random();

            if (rand.Next(0, 2) == 0)
            {
                name = "Rectangle";
            }
            else
            {
                name = "Ellipse";
            }*/
            InitializeComponent();
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Draw(GraphObject go)
        {
            elements.Add(go);
            this.panel1.Invalidate();
        }

        private void AddFigure(object sender, EventArgs e)
        {
            Draw(factory.CreateGraphObject());
        }

        private void ClearFigures(object sender, EventArgs e)
        {
            elements.Clear();
            Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            foreach (GraphObject elem in elements)
            {
                elem.Draw(e.Graphics);
            }

        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            GraphObject mouseDrawFactory = factory.CreateGraphObject();

            try
            {
                mouseDrawFactory.X = e.X;
                mouseDrawFactory.Y = e.Y;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Incorrect coord");
            }
            elements.Add(mouseDrawFactory);

            /*if (name == "Rectangle")
            {
                Rectangle go = new Rectangle();
                try
                {
                    go.X = e.X;
                    go.Y = e.Y;
                }
                catch (ArgumentException ex) 
                { 
                    MessageBox.Show("Incorrect coord"); 
                }
                elements.Add(go);
            }
            else if (name == "Ellipse")
            {
                Ellipse go = new Ellipse();
                try
                {
                    go.X = e.X;
                    go.Y = e.Y;
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Incorrect coord");
                }
                elements.Add(go);
            }*/

            this.panel1.Invalidate();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"Координаты: {e.X}, {e.Y}";
        }

        private void Select(object sender, MouseEventArgs e)
        {
            if (elements.Count > 0)
            {
                int i = 0;
                foreach (GraphObject element in elements)
                {
                    if (element.ContainsPoint(e.Location))
                    {
                        id = i;
                    }
                    element.Selected = false;
                    i++;
                }
                if (id > -1)
                {
                    elements[id].Selected = true;
                    this.panel1.Invalidate();
                }

            }
        }

        private void вверхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (id > -1)
            {
                elements[id].Y -= 5;
                this.panel1.Invalidate();
            }
        }

        private void внизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (id > -1)
            {
                elements[id].Y += 5;
                this.panel1.Invalidate();
            }
        }

        private void вправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (id > -1)
            {
                elements[id].X += 5;
                this.panel1.Invalidate();
            }
        }

        private void влевоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (id > -1)
            {
                elements[id].X -= 5;
                this.panel1.Invalidate();
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (id > -1)
            {
                elements.Remove(elements[id]);
                id = -1;
                this.panel1.Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchMenuChecked(sender);
            factory = new DefaultFactory();
        }


        private void twoTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchMenuChecked(sender);
            factory = new TwoTypeFactory();
        }

        private void randomObjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchMenuChecked(sender);
            factory = new RandomObjectFactory();
        }

        private void SwitchMenuChecked(object sender)
        {
            defaultToolStripMenuItem.Checked = (sender == defaultToolStripMenuItem);
            twoTypesToolStripMenuItem.Checked = (sender == twoTypesToolStripMenuItem);
            randomObjToolStripMenuItem.Checked = (sender == randomObjToolStripMenuItem);

            defaultToolStripMenuItem.CheckState = (sender == defaultToolStripMenuItem) ? CheckState.Checked : CheckState.Unchecked;
            twoTypesToolStripMenuItem.CheckState = (sender == twoTypesToolStripMenuItem) ? CheckState.Checked : CheckState.Unchecked;
            randomObjToolStripMenuItem.CheckState = (sender == randomObjToolStripMenuItem) ? CheckState.Checked : CheckState.Unchecked;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '+')
            {
                Draw(factory.CreateGraphObject());
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Draw(factory.CreateGraphObject());

            /*if (name == "Rectangle")
            {
                Rectangle go = new Rectangle();
                Draw(go);
            }
            else if (name == "Ellipse")
            {
                Ellipse go = new Ellipse();
                Draw(go);
            }*/
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            elements.Clear();
            this.panel1.Invalidate();
        }
    
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (id> -1)
            {

                foreach(Queue<QueueObjects> queueOfArrayOfQueue in arrayOfQueue)
                {
                    if (queueOfArrayOfQueue.Count > 0)
                    {
                        QueueObjects structOfMove = queueOfArrayOfQueue.Dequeue();
                        elements[structOfMove.ID].X = structOfMove.x;
                        elements[structOfMove.ID].Y = structOfMove.y;
                    }
                }

                for (int i = arrayOfQueue.Count-1; i >= 0; i--)
                {
                    if (arrayOfQueue[i].Count == 0)
                    {
                        arrayOfQueue.RemoveAt(i);
                    }
                }

                if (arrayOfQueue.Count == 0)
                {
                    timer1.Enabled = false;
                }
            }
                this.panel1.Invalidate();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (id > -1 && e.Button == MouseButtons.Right)
            {
                Queue<QueueObjects> currentQueque = new Queue<QueueObjects>();
                int x1;
                int y1;

                if (mappingIdQueue.ContainsKey(elements[id]))
                {
                    currentQueque = mappingIdQueue.GetValueOrDefault(elements[id]);
                    if (currentQueque.Count > 0)
                    {
                        QueueObjects lastStructcurrentQueque = currentQueque.Last();
                        x1 = lastStructcurrentQueque.x;
                        y1 = lastStructcurrentQueque.y;
                    }
                    else
                    {
                        x1 = elements[id].X;
                        y1 = elements[id].Y;

                        arrayOfQueue.Add(currentQueque);
                    }
                }
                else
                {
                    mappingIdQueue.Add(elements[id], currentQueque);
                    arrayOfQueue.Add(currentQueque);

                    x1 = elements[id].X;
                    y1 = elements[id].Y;
                }

                    
                    int x2 = e.X;
                    int y2 = e.Y;

                    if (elements[id] is Rectangle)
                    {
                        x2 -= 25;
                        y2 -= 25;
                    }

                    if (elements[id] is Ellipse)
                    {
                        x2 -= 40;
                        y2 -= 30;
                    }

                    int dx = x2 - x1;
                    int dy = y2 - y1;
                    int deltaX = Math.Abs(x2 - x1);
                    int deltaY = Math.Abs(y2 - y1);
                    int signX = x1 < x2 ? 1 : -1;
                    int signY = y1 < y2 ? 1 : -1;
                    int error = deltaX - deltaY;


                    while (x1 != x2 || y1 != y2)

                    {
                        QueueObjects h = new QueueObjects();

                        int error2 = error * 2;

                        if (error2 > -deltaY)

                        {

                            error -= deltaY;

                            x1 += signX;

                        }

                        if (error2 < deltaX)

                        {

                            error += deltaX;

                            y1 += signY;

                        }

                        
                        h.ID = id;
                        h.x = x1;
                        h.y = y1;
                        currentQueque.Enqueue(h);

                    }

                timer1.Interval = 10;
                timer1.Enabled = true;

            } 
        }
    }
}
