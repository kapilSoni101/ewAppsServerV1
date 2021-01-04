/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 30 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.CommonService {

    //This Helper Class is Used For Currency Group
    public class CurrencyPicklist {

        public int CurrencyId {
            get; set;
        }

        public string CurrencyName {
            get; set;
        }

        public string ShortName {
            get; set;
        }

        public string Symbol {
            get; set;
        }

        public List<CurrencyPicklist> GetCurrencyPL() {
            List<CurrencyPicklist> list = new List<CurrencyPicklist>();
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.None, CurrencyName = "Select from the list", ShortName = "NA", Symbol = "" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.USDollar, CurrencyName = "United States Dollar USD $", ShortName = "USD", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.ArgentinaPeso, CurrencyName = "Argentina Peso ARS $", ShortName = "ARS", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.AustraliaDollar, CurrencyName = "Australia Dollar AUD $", ShortName = "AUD", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.BahamasDollar, CurrencyName = "Bahamas Dollar BSD $", ShortName = "BSD", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.BoliviaBolíviano, CurrencyName = "Bolivia Bolíviano BOB $b", ShortName = "BOB", Symbol = "$b" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.BrazilReal, CurrencyName = "Brazil Real BRL R$", ShortName = "BRL", Symbol = "R$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.CanadaDollar, CurrencyName = "Canada Dollar CAD $", ShortName = "CAD", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.ChilePeso, CurrencyName = "Chile Peso CLP $", ShortName = "CLP", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.ChinaYuanRenminbi, CurrencyName = "China Yuan Renminbi CNY ¥", ShortName = "CNY", Symbol = "¥" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.ColombiaPeso, CurrencyName = "Colombia Peso COP $", ShortName = "COP", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.CubaPeso, CurrencyName = "Cuba Peso CUP ?", ShortName = "CUP", Symbol = "?" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.DenmarkKrone, CurrencyName = "Denmark Krone DKK kr", ShortName = "DKK", Symbol = "kr" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.EgyptPound, CurrencyName = "Egypt Pound EGP £", ShortName = "EGP", Symbol = "£" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.ElSalvadorColon, CurrencyName = "El Salvador Colon SVC $", ShortName = "SVC", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.Euro, CurrencyName = "European Countries Euro €", ShortName = "EURO", Symbol = "€" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.HongKongDollar, CurrencyName = "Hong Kong Dollar HKD $", ShortName = "HKD", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.IndianRupee, CurrencyName = "Indian Rupee INR ?", ShortName = "INR", Symbol = "?" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.JapanYen, CurrencyName = "Japan Yen JPY ¥", ShortName = "JPY", Symbol = "¥" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.LebanonPound, CurrencyName = "Lebanon Pound LBP £", ShortName = "LBP", Symbol = "£" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.MauritiusRupee, CurrencyName = "Mauritius Rupee MUR ?", ShortName = "MUR", Symbol = "?" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.MexicoPeso, CurrencyName = "Mexico Peso MXN $", ShortName = "MXN", Symbol = "$" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.QatarRiyal, CurrencyName = "Qatar Riyal QAR ?", ShortName = "QAR", Symbol = "?" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.RussiaRuble, CurrencyName = "Russia Ruble RUB ?", ShortName = "RUB", Symbol = "?" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.SaudiArabiaRiyal, CurrencyName = "Saudi Arabia Riyal SAR ?", ShortName = "SAR", Symbol = "?" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.SouthAfricaRand, CurrencyName = "South Africa Rand ZAR R", ShortName = "ZAR", Symbol = "R" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.SwitzerlandFranc, CurrencyName = "Switzerland Franc CHF ?", ShortName = "CHF", Symbol = "?" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.TurkeyLira, CurrencyName = "Turkey Lira TRY ?", ShortName = "TRY", Symbol = "?" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.Pound, CurrencyName = "United Kingdom Pound GBP £", ShortName = "GBP", Symbol = "£" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.VenezuelaBolívar, CurrencyName = "Venezuela Bolívar VEF Bs", ShortName = "VEF", Symbol = "Bs" });
            list.Add(new CurrencyPicklist() { CurrencyId = (int)CurrencyEnum.YemenRial, CurrencyName = "Yemen Rial YER ?", ShortName = "YER", Symbol = "?" });

            return list;

        }
    }
}
