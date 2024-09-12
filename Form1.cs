using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace BlockChain
{
    public partial class Form1 : Form
    {
        class Block
        {
            int id; // Block ID
            int nonce;
            string data; // this will change
            string prev;
            string hash;
            public Block()
            {
                id = 0;
                nonce = 0;
                data = "";
                prev = "0000000000000000000000000000000000000000000000000000000000000000";
                UpdateHash();
            }
            public int ID
            {
                get { return id; }  
                set { id = value; }
            }
            public int Nonce
            {
                get { return nonce; }
                set { nonce = value; }
            }
            public string Data
            {
                get { return data; }
                set { data = value; }
            }
            public string Prev
            {
                get { return prev; }
                set { prev = value; }
            }
            public string Hash
            {
                get { return hash; }
                set { hash = value; }
            }
            public void UpdateHash()
            {
                string plainData = id.ToString() + nonce.ToString() + data + prev;
                hash = ComputeSha256Hash(plainData);
            }
            public void Mine()
            {
                int i = 0;
                while (hash.Substring(0, 5) != "00000")
                {
                    ++i;
                    string plainData = id.ToString() + i.ToString() + data + prev;
                    hash = ComputeSha256Hash(plainData);
                }
                nonce = i;
            }
        }



        System.Collections.ArrayList BlockChain;
        Block DisplayBlock;
        bool Updating;



        public Form1()
        {
            InitializeComponent();

            BlockChain = new System.Collections.ArrayList();

            this.Text = "Block chain";

            Block GenesisBlock = new Block();
            BlockChain.Add(GenesisBlock);
           
            Block block = new Block();
            block.ID = 1;
            // What about previous block hash?
            Block PriorBlock = (Block)BlockChain[BlockChain.Count - 1];
            block.Prev = PriorBlock.Hash;
            block.UpdateHash();
            BlockChain.Add(block);

            block = new Block();
            block.ID = 2;
            // What about previous block hash?
            PriorBlock = (Block)BlockChain[BlockChain.Count - 1];
            block.Prev = PriorBlock.Hash;
            block.UpdateHash();
            BlockChain.Add(block);

            block = new Block();
            block.ID = 3;
            // What about previous block hash?
            PriorBlock = (Block)BlockChain[BlockChain.Count - 1];
            block.Prev = PriorBlock.Hash;
            block.UpdateHash();
            BlockChain.Add(block);

            block = new Block();
            block.ID = 4;
            // What about previous block hash?
            PriorBlock = (Block)BlockChain[BlockChain.Count - 1];
            block.Prev = PriorBlock.Hash;
            block.UpdateHash();
            BlockChain.Add(block);

            DisplayBlock = GenesisBlock;
            ShowBlock(DisplayBlock.ID);
            //ShowBlock(1);

            // Middle block fields
            textBox3.Enabled = false;
            textBox2.Enabled = false;
            textBox5.Enabled = false;
            textBox4.Enabled = false;
            textBox6.Enabled = false;

            // Right block fields
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false; // Data textbox
            textBox11.Enabled = false;
            textBox12.Enabled = false;

            textBox18.Enabled = false;
            textBox17.Enabled = false;
            textBox13.Enabled = false;
            textBox14.Enabled = false;
            textBox15.Enabled = false;

            Updating = false;
        }



        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }



        private void ShowBlock(int BlockID)
        {
            // Clear 'left' block
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            panel3.BackColor = Color.AntiqueWhite;

            // Find 'left' block
            Block blockLeft = null;
            foreach (Block b in BlockChain)
            {
                if (b.ID == (BlockID - 1))
                {
                    blockLeft = b;
                    break;
                }
            }

            // Display 'left' block
            if (blockLeft != null)
            {
                textBox13.Text = blockLeft.Data;
                textBox17.Text = blockLeft.Nonce.ToString();
                textBox18.Text = blockLeft.ID.ToString();
                textBox14.Text = blockLeft.Hash;
                textBox15.Text = blockLeft.Prev;
                if (blockLeft.Hash.Substring(0, 5) == "00000")
                    panel3.BackColor = Color.FromArgb(208, 255, 208);
                else
                    panel3.BackColor = Color.FromArgb(255, 208, 208);
            }
            else
            {
 
            }

            // Find 'middle' block
            Block block = null;
            foreach (Block b in BlockChain)
            {
                if (b.ID == BlockID)
                {
                    block = b;
                    break;
                }
            }

            // Display 'middle' block
            textBox1.Text = block.Data;
            textBox4.Text = block.Nonce.ToString();
            textBox5.Text = block.ID.ToString();
            textBox2.Text = block.Hash;
            textBox6.Text = block.Prev;
            if (block.Hash.Substring(0, 5) == "00000")
                panel2.BackColor = Color.FromArgb(208, 255, 208);
            else
                panel2.BackColor = Color.FromArgb(255, 208, 208);

            // Clear 'right' block
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            panel1.BackColor = Color.AntiqueWhite;

            // Find 'right' block
            Block block2 = null;
            foreach (Block b in BlockChain)
            {
                if (b.ID == (BlockID + 1))
                {
                    block2 = b;
                    break;
                }
            }

            // Display 'right' block
            if (block2!=null)
            {
                textBox10.Text = block2.Data;
                textBox9.Text = block2.Nonce.ToString();
                textBox8.Text = block2.ID.ToString();
                textBox12.Text = block2.Hash;
                textBox11.Text = block2.Prev;
                if (block2.Hash.Substring(0, 5) == "00000")
                    panel1.BackColor = Color.FromArgb(208, 255, 208);
                else
                    panel1.BackColor = Color.FromArgb(255, 208, 208);
            }
            else
            {
                //textBox10.Visible = false;
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Updating == false)
            {
                DisplayBlock.Data = textBox1.Text;
                DisplayBlock.UpdateHash();
                // if we are not last block in chain
                if (DisplayBlock.ID < BlockChain.Count - 1)
                {
                    string PriorHash = DisplayBlock.Hash;
                    Block block = Find(DisplayBlock.ID + 1);
                    while (block!=null)// Loop through remainder of blockchain
                    {
                        block.Prev = PriorHash; // Update Prev hash in next block
                        block.UpdateHash();
                        PriorHash = block.Hash;
                        block = Find(block.ID + 1);
                    }
                }
                ShowBlock(DisplayBlock.ID);
            }
        }



        // Mine button
        private void button1_Click(object sender, EventArgs e)
        {
            DisplayBlock.Mine();

            if (DisplayBlock.ID < BlockChain.Count - 1)
            {
                string PriorHash = DisplayBlock.Hash;
                Block block = Find(DisplayBlock.ID + 1);
                while (block != null)// Loop through remainder of blockchain
                {
                    block.Prev = PriorHash; // Update Prev hash in next block
                    block.UpdateHash();
                    PriorHash = block.Hash;
                    block = Find(block.ID + 1);
                }
            }

            ShowBlock(DisplayBlock.ID);
        }


        private Block Find(int ID)
        {
            foreach (Block b in BlockChain)
            {
                if (b.ID == ID)
                    return b;
            }
            return null;
        }



        // Next button
        private void button2_Click(object sender, EventArgs e)
        {
            Updating = true;
            foreach (Block b in BlockChain)
            {
                if (b.ID == (DisplayBlock.ID + 1))
                {
                    DisplayBlock = b;
                    break;
                }
            }    
            ShowBlock(DisplayBlock.ID);
            Updating = false;
        }



        // Prior button
        private void button3_Click(object sender, EventArgs e)
        {
            Updating = true;
            foreach (Block b in BlockChain)
            {
                if (b.ID == (DisplayBlock.ID - 1))
                {
                    DisplayBlock = b;
                    break;
                }
            }  
            ShowBlock(DisplayBlock.ID);
            Updating = false;
        }

    }
}
