﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using SystemColors = System.Drawing.SystemColors;
using System.Drawing;

namespace kpsWindow
{
    class kpsbuttonHandler
    {

        private int noOfKeys;
        private List<Button> buttons = new List<Button>();
        private List<Keys> keys;

        private Dictionary<Keys, Button> hmap = new Dictionary<Keys, Button>();
        private TextBox tb;

        private kpsForm form;
        private LabelHandler kpsLh;
        private LabelHandler maxkpsLh;

        private LabelHandler configLabel;
        private Image img;
        private Bitmap bmap;
        private CheckBox cBox;

        private bool useImg = true;

        private int bHeight;
        private int bWidth;

        private int configButtonCount = 0;
        private bool inConfigMode = false;

        private kpsGraphics kpsGraphics; //only assign this/to this if 2nd overload is used

        public kpsbuttonHandler(kpsForm form)
        {
            this.bHeight = 40;
            this.bWidth = 40;
            this.form = form;
            this.noOfKeys = form.noOfKeys;
            this.cBox = form.Hgraphics.getCboxWrapper1().getCbox();
            kpsLh = new LabelHandler(form, false);
            maxkpsLh = new LabelHandler(form, true);
            configLabel = new LabelHandler(form);

            this.keys = fileHandler.GetKeys(noOfKeys);

            try
            {
                img = Image.FromFile(fileHandler.path + "\\presseffect.png");
                this.img = (Image)new Bitmap(img, new Size(bWidth, bHeight));

              
            }
            catch (Exception e) {
             
           
                this.useImg = false;
                
            
            }


           

                    
            tb = new TextBox();


            // form.KeyDown += new KeyEventHandler(onKeydown);
            // form.KeyUp += new KeyEventHandler(onKeyup);
            form.Select();


            loadConfigButton();
           

        }


        public kpsbuttonHandler(kpsGraphics kpsGraphics) : this(kpsGraphics.getForm()) {

            this.kpsGraphics = kpsGraphics;
            

        }










        public void reInitButtonKeys() {


            var vButtons = hmap.Values.ToList();

            //delete all the buttons first
            for (int i = 0; i < noOfKeys; i++) {
                form.Controls.Remove(vButtons[i]);
                setKeyMap();
            }

            //add back the updated buttons from hmap after setKeymap is called which references an updated keys list updated after the last onconfigKeyup eventhandler is called
            //where configbuttoncount == noOfkeys
            initializeButtonKeys();
        
        
        }

        public void initializeButtonKeys()
        {




            int centreX = (int)form.width / 2;
            int centreY = (int)form.height / 2;
            switch (noOfKeys)
            {
                case 4:
                   /* 
                    keys.Add(Keys.D);
                    keys.Add(Keys.F);
                    keys.Add(Keys.J);
                    keys.Add(Keys.K);
                   */
                    setKeyMap();
                    var vButtons = hmap.Values.ToList();
                    var vKeys = hmap.Keys.ToList();

                    for (int i = 0; i < 4; i++)
                    {
                        Button button = vButtons[i];

                        button.Text = vKeys[i].ToString();
                        button.ForeColor = Color.LawnGreen;

                        //  button.KeyDown += new KeyEventHandler(onKeydown);
                        //  button.KeyUp += new KeyEventHandler(onKeyup);

                        button.Location = new System.Drawing.Point(centreX + (i - 2) * button.Size.Width, centreY);
                        form.Controls.Add(button);
                    }


                    break;
                case 7:
                   /*
                    keys.Add(Keys.S);
                    keys.Add(Keys.D);
                    keys.Add(Keys.F);
                    keys.Add(Keys.Space);
                    keys.Add(Keys.J);
                    keys.Add(Keys.K);
                    keys.Add(Keys.L);
                   */





                    setKeyMap();
                    vButtons = hmap.Values.ToList();
                    vKeys = hmap.Keys.ToList();

                    for (int i = 0; i < 7; i++)
                    {
                        Button button = vButtons[i];

                        button.Text = vKeys[i].ToString();
                        button.ForeColor = Color.LawnGreen;
                        // button.KeyDown += new KeyEventHandler(onKeydown);
                        // button.KeyUp += new KeyEventHandler(onKeyup);


                        button.Location = new System.Drawing.Point(bWidth * 4 + (i - 4) * button.Size.Width, centreY);
                        form.Controls.Add(button);



                    }


                    break;








            }
        }

        //deprecated
        private List<Button> setButtons()
        {

            for (int i = 0; i < noOfKeys; i++)
            {
                Button button = new Button();
                button.Size = new Size(bWidth, bHeight);
                buttons.Add(button);
            }

            return buttons;


        }
        private Dictionary<Keys, Button> setKeyMap()
        {
            if (hmap.Count != 0) {
                hmap.Clear();

            }
            var vkeys = keys;


            for (int i = 0; i < noOfKeys; i++)
            {
                Button button = new Button();
                button.Size = new Size(bWidth, bHeight);
               button.BackColor = Color.Black;
                hmap.Add(vkeys[i], button);


            }

            return hmap;
        }
        //used before setKeyMap() is called.
        private void loadConfigButton()
        {

            Button configbutton = new Button();
            configbutton.Size = new Size(100, 20); //45,20 default non kps-button size
                                                  // configbutton.Location = new Point(form.width, form.height - configbutton.Height);
            configbutton.Text = "Key Config";
            configbutton.BackColor = Color.Black;
            configbutton.ForeColor = Color.LawnGreen;
            configbutton.Location = new Point(form.width-configbutton.Size.Width,0);

            configbutton.MouseClick += onMouseClick;

            form.Controls.Add(configbutton);

        }

        private void onMouseClick(Object o, MouseEventArgs e)
        {
            if (inConfigMode == false) {
                keys.Clear();
                form.HookM.Unsubscribe(false);

                form.Controls.Remove(kpsLh.getLabel());
                form.Controls.Remove(maxkpsLh.getLabel());
                form.Controls.Remove(cBox); //needs testing


                configLabel.configurationLabel();
                form.HookM.SubscribeConfig();
                inConfigMode = true;
            }






        }



        public void onKeydown(Object o, KeyEventArgs e)
        {
           
            var vKeys = hmap.Keys.ToList();
            var vButtons = hmap.Values.ToList();

            Button randbutton = new Button();

            if (e.KeyCode == vKeys[0])
            {

                randbutton = vButtons[0];

            }
            else if (e.KeyCode == vKeys[1])
            {
                randbutton = vButtons[1];

            }
            else if (e.KeyCode == vKeys[2])
            {
                randbutton = vButtons[2];

            }
            else if (e.KeyCode == vKeys[3])
            {   

                randbutton = vButtons[3];

            }
            else if (noOfKeys == 7)
            {
                if (e.KeyCode == vKeys[4])
                {

                    randbutton = vButtons[4];

                }
                else if (e.KeyCode == vKeys[5])
                {
                    randbutton = vButtons[5];

                }
                else if (e.KeyCode == vKeys[6])
                {
                    randbutton = vButtons[6];

                }




            }

            //calculations must be done on keyup method to prevent spam increase in kps when holding down key.
            //  randbutton.BackColor = Color.Transparent;
            if (useImg)
            {
                randbutton.BackgroundImage = img;
            }
            else {
                randbutton.ForeColor = Color.White;
            }
            





            //**other particular keydown event method may also be attached to this hook,
            //**so if this event is handled, only this keydown event method will run, the rest will not since this has handled the event. thus handled is set to false
           
                kpsLh.configureKpsLabel();
                maxkpsLh.configureHighestKpsLabel();
            

        }

        //event handler 'onkeypress' is only used for configuring keys
        public void onConfigKeyUp(object o, KeyEventArgs e) {

            var vbuttons = hmap.Values.ToList();

            Console.WriteLine(keys);
            Keys eKey = e.KeyCode;
            keys.Add(eKey);
            vbuttons[configButtonCount].Select();
            configButtonCount++;
            
           // Console.WriteLine(keys);
            Console.WriteLine(eKey);
            

            if (configButtonCount == noOfKeys) {
                Console.WriteLine(configButtonCount);
                form.HookM.unSubscribeConfig();

                form.Controls.Remove(configLabel.getLabel());
                reInitButtonKeys();
                form.Controls.Add(cBox);
                cBox.Select();
                form.Controls.Add(kpsLh.getLabel());
                form.Controls.Add(maxkpsLh.getLabel());
               
                form.HookM.Subscribe();

                fileHandler.updateKeyData(noOfKeys,keys);
                

                configButtonCount = 0;
                inConfigMode = false;
                keys.Clear();

            }
        
        
        
        
        }
        public void onKeyup(object o, KeyEventArgs e)
        {

            var vKeys = hmap.Keys.ToList();
            var vButtons = hmap.Values.ToList();

            Button randbutton = new Button();
            if (!vKeys.Contains(e.KeyCode)) { return; }
            if (e.KeyCode == vKeys[0])
            {
                randbutton = vButtons[0];
                randbutton.Text = vKeys[0].ToString();

            }
            else if (e.KeyCode == vKeys[1])
            {
                randbutton = vButtons[1];
                randbutton.Text = vKeys[1].ToString();
            }
            else if (e.KeyCode == vKeys[2])
            {
                randbutton = vButtons[2];
                randbutton.Text = vKeys[2].ToString();
            }


            else if (e.KeyCode == vKeys[3])
            {
                randbutton = vButtons[3];
                randbutton.Text = vKeys[3].ToString();
            }
            else if (noOfKeys == 7)
            {

                if (e.KeyCode == vKeys[4])
                {

                    randbutton = vButtons[4];
                    randbutton.Text = vKeys[4].ToString();
                }
                else if (e.KeyCode == vKeys[5])
                {
                    randbutton = vButtons[5];
                    randbutton.Text = vKeys[5].ToString();
                }
                else if (e.KeyCode == vKeys[6])
                {
                    randbutton = vButtons[6];
                    randbutton.Text = vKeys[6].ToString();
                }




            }
            
                randbutton.BackColor = Color.Black;
            if (useImg) {
                randbutton.BackgroundImage = null;
            }
            else { randbutton.ForeColor = Color.LawnGreen;
            }
            //Console.WriteLine("is pressed.");
            kpsCalculator.keysPerNs++;





            


        }

    }




    
}
