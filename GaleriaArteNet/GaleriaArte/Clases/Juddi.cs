/*
 * Created by SharpDevelop.
 * User: ShadowLink
 * Date: 05/03/2015
 * Time: 16:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows.Forms.VisualStyles;

namespace GaleriaArte.Clases
{
	/// <summary>
	/// Description of Juddi.
	/// </summary>
	public class Juddi
	{
		public Juddi()
		{
			
		}
		
		public string getServiceUrl(string name){
			serviceInfo s = new serviceInfo();
        	UDDIInquiryService juddi = new UDDIInquiryService();
        	find_service fs = new find_service();
        	fs.findQualifiers = new []{"approximateMatch"};
        	name n = new name();
        	n.Value = name;
        	n.lang = "en";
        	fs.name = new []{n};
        	serviceList services = juddi.find_service(fs);
    
        	for(int i = 0; i<services.serviceInfos.Count(); i++){
        		s = services.serviceInfos[i];
        	}
        	
        	
        	get_serviceDetail sd= new get_serviceDetail();
        	sd.serviceKey = new string[]{s.serviceKey};
        	serviceDetail service = juddi.get_serviceDetail(sd);
        	string url = service.businessService[0].bindingTemplates[0].accessPoint.Value;
        	
        	if(checkUrl(url)){
        		url = url;
        	}else{
        		url =url;
        	}
        	
        	return url;
		}
		
		private bool checkUrl(string url){
			bool isAlive = true;
			WebRequest request = WebRequest.Create(url);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			if (response == null || response.StatusCode != HttpStatusCode.OK){
				isAlive = false;
			}
			response.Close();
			return isAlive;
		}
	}
}
