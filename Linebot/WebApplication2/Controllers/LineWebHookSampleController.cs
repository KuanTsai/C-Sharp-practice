using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class LineBotWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        const string channelAccessToken = "mT1+MeHamE9Iy/pys2Mlnorzsb2mrRHxQPZgg6MwJ8h1w7LOV4mHqUME45K0uUa9O4OOynIptNqk156bKWr51ZXMBEXqKZ7pXTzcRH0YY5DABowHsB9wlD7ZG7gaCS1OgTsve08JqTyDgrYF7o+aGAdB04t89/1O/w1cDnyilFU=";
        const string AdminUserId = "Ua794d5fc8c988c9bc80a19ffe71ca4f5";

        [Route("api/LineWebHookSamplecode")]
        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                //設定ChannelAccessToken(或抓取Web.Config)
                this.ChannelAccessToken = channelAccessToken;
                //取得Line Event(範例，只取第一個)
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                //回覆訊息
                if (LineEvent.type == "message")
                {
                    if (LineEvent.message.type == "text") //收到文字
                        this.ReplyMessage(LineEvent.replyToken, "你說了:" + LineEvent.message.text);
                    if (LineEvent.message.type == "sticker") //收到貼圖
                        this.ReplyMessage(LineEvent.replyToken, 1, 117);
                    if (LineEvent.message.type == "Location")
                        this.ReplyMessage(LineEvent.replyToken, LineEvent.message.address);
                }
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //如果發生錯誤，傳訊息給Admin
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}
