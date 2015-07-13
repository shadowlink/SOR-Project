/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sor.galeriaarte.clases;

import org.uddi.api_v3.FindQualifiers;
import org.uddi.api_v3.FindService;
import org.uddi.api_v3.GetServiceDetail;
import org.uddi.api_v3.Name;
import org.uddi.api_v3.ServiceDetail;
import org.uddi.api_v3.ServiceInfo;
import org.uddi.api_v3.ServiceInfos;
import org.uddi.api_v3.ServiceList;
import org.uddi.v3_service.UDDIInquiryService;


/**
 *
 * @author ShadowLink
 */
public class Juddi {
    
    public String getServiceUrl(String name){
        ServiceInfo s = new ServiceInfo();
        UDDIInquiryService juddi = new UDDIInquiryService();
        FindService fs = new FindService();
        FindQualifiers fq = new FindQualifiers();
        fq.getFindQualifier().add("approximateMatch");
        Name n = new Name();
        n.setValue(name);
        n.setLang("en");
        fs.getName().add(n);
        fs.setFindQualifiers(fq);
        
        ServiceList services = juddi.getUDDIInquiryImplPort().findService(fs);
        ServiceInfos si = services.getServiceInfos();
        for(int i=0; i<si.getServiceInfo().size(); i++){
            s = si.getServiceInfo().get(i);
        }
        
        GetServiceDetail sd = new GetServiceDetail();
        sd.getServiceKey().add(s.getServiceKey());
        ServiceDetail service = juddi.getUDDIInquiryImplPort().getServiceDetail(sd);
        String url = service.getBusinessService().get(0).getBindingTemplates().getBindingTemplate().get(0).getAccessPoint().getValue();
        
        
        return url;
    }
}
