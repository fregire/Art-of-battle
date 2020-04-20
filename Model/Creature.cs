﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Art_of_battle.Model
{
    public class Creature
    {
        public int Health { get; set; }
        public int CurrHealth { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public Point Position { get; set; }
    }
}
