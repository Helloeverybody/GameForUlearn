﻿using System.ComponentModel;
using System.Windows.Forms;

namespace My_game_for_Ulearn
{
    public class Dialog : UserControl
    {
        private static MainForm mainForm;
        private IContainer components;
        
        public Dialog(MainForm form)
        {
            mainForm = form;
            ClientSize = Screen.PrimaryScreen.Bounds.Size;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            //DrawGame(sender, e); надо бы за диалогом игру отрисовывать....
            var a = new Model.Dialog("Это тестовоый диалог. Это тестовоый диалог. Это тестовоый диалог. Это тестовоый диалог.");
            a.DrawDialog(g, mainForm.Size);
        }
    }
}