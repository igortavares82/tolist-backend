﻿using System;
using Standard.ToList.Model.SeedWork;

        public void Update(decimal current) => Current = current;

        public bool IsDesired() => Current <= Desired;

        public bool IsLower() => Current <= Price;
    }