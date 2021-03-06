﻿/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Pay;
using ZyGames.Framework.Game.Service;

namespace ZyGames.Framework.Game.Contract.Action
{
    public class Sdk91PayAction : BaseStruct
    {
        private string OrderID = string.Empty;
        private int gameID = 0;
        private int serviceID = 0;
        private string passportId = string.Empty;
        private string servicename = string.Empty;
        private string _RetailID = "0000";

        public Sdk91PayAction(short aActionId, HttpGet httpGet)
            : base(aActionId, httpGet)
        {
        }

        public override void BuildPacket()
        {
        }

        public override bool GetUrlElement()
        {
            TraceLog.ReleaseWriteFatal("url");
            if (httpGet.GetString("OrderID", ref OrderID)
                && httpGet.GetInt("gameID", ref gameID)
                && httpGet.GetInt("Server", ref serviceID)
                && httpGet.GetString("ServiceName", ref servicename)
                && httpGet.GetString("PassportID", ref passportId))
            {
                httpGet.GetString("RetailID", ref _RetailID);
                return true;
            }
            return false;
        }

        public override bool TakeAction()
        {
            SaveLog(string.Format("91SKD充值>>Order:{0},Pid:{1},servicename:{2}", OrderID, passportId, servicename));
            //PaymentService.Get91Payment(gameID, serviceID, passportId, servicename, OrderID);

            PayManager.get91PayInfo(gameID, serviceID, passportId, servicename, OrderID, _RetailID);
            return true;
        }
    }
}