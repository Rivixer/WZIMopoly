﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using WZIMopoly.Enums;
using WZIMopoly.Utils;
#endregion

namespace WZIMopoly.Source.Board.Map.Tiles
{

    class Subject : PurchasableTile
    {
        public SubjectGrade Grade;
        public readonly int UpgradePrice;
        public readonly Dictionary<SubjectGrade, int> TaxPrices;
        public readonly SubjectColor Color;

        public Subject(XmlNode node) : base(node)
        {
            Grade = SubjectGrade.Three;
            UpgradePrice = int.Parse(node.SelectSingleNode("upgrade_price").InnerText);
            TaxPrices = new Dictionary<SubjectGrade, int>();

            foreach (XmlAttribute attribute in node.SelectSingleNode("tax_prices").Attributes)
            {
                if (!Enum.TryParse(attribute.Name, true, out SubjectGrade temp))
                {
                    throw new ArgumentException($"Invalid attribute name in tax_prices node in node with {Id} id");
                }
                TaxPrices.Add(temp, int.Parse(attribute.InnerText));
            }

            string rawColor = NamingConvention.ConvertSnakeCaseToPascalCase(node.SelectSingleNode("color").InnerText);
            Debug.WriteLine(rawColor);
            if (!Enum.TryParse(rawColor, true, out Color))
            {
                throw new ArgumentException($"Invalid color node in node with {Id} id");
            }
        }
        public override void OnStand(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
