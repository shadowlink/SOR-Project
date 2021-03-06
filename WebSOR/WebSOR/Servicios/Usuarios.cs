﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.0
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// Este código fuente fue generado automáticamente por wsdl, Versión=4.0.30319.33440.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="UsuariosSoap", Namespace="http://tempuri.org/")]
public partial class Usuarios : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback tryLoginOperationCompleted;
    
    private System.Threading.SendOrPostCallback getUserOperationCompleted;
    
    private System.Threading.SendOrPostCallback newUserOperationCompleted;
    
    private System.Threading.SendOrPostCallback updateUserOperationCompleted;
    
    /// <remarks/>
    public Usuarios() {
        this.Url = "http://localhost:56660/Usuarios.asmx";
    }
    
    /// <remarks/>
    public event tryLoginCompletedEventHandler tryLoginCompleted;
    
    /// <remarks/>
    public event getUserCompletedEventHandler getUserCompleted;
    
    /// <remarks/>
    public event newUserCompletedEventHandler newUserCompleted;
    
    /// <remarks/>
    public event updateUserCompletedEventHandler updateUserCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/tryLogin", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public bool tryLogin(string mail, string pass, int priv) {
        object[] results = this.Invoke("tryLogin", new object[] {
                    mail,
                    pass,
                    priv});
        return ((bool)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegintryLogin(string mail, string pass, int priv, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("tryLogin", new object[] {
                    mail,
                    pass,
                    priv}, callback, asyncState);
    }
    
    /// <remarks/>
    public bool EndtryLogin(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((bool)(results[0]));
    }
    
    /// <remarks/>
    public void tryLoginAsync(string mail, string pass, int priv) {
        this.tryLoginAsync(mail, pass, priv, null);
    }
    
    /// <remarks/>
    public void tryLoginAsync(string mail, string pass, int priv, object userState) {
        if ((this.tryLoginOperationCompleted == null)) {
            this.tryLoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OntryLoginOperationCompleted);
        }
        this.InvokeAsync("tryLogin", new object[] {
                    mail,
                    pass,
                    priv}, this.tryLoginOperationCompleted, userState);
    }
    
    private void OntryLoginOperationCompleted(object arg) {
        if ((this.tryLoginCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.tryLoginCompleted(this, new tryLoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getUser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string getUser(string mail) {
        object[] results = this.Invoke("getUser", new object[] {
                    mail});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegingetUser(string mail, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("getUser", new object[] {
                    mail}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndgetUser(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void getUserAsync(string mail) {
        this.getUserAsync(mail, null);
    }
    
    /// <remarks/>
    public void getUserAsync(string mail, object userState) {
        if ((this.getUserOperationCompleted == null)) {
            this.getUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetUserOperationCompleted);
        }
        this.InvokeAsync("getUser", new object[] {
                    mail}, this.getUserOperationCompleted, userState);
    }
    
    private void OngetUserOperationCompleted(object arg) {
        if ((this.getUserCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.getUserCompleted(this, new getUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/newUser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public bool newUser(string userJson) {
        object[] results = this.Invoke("newUser", new object[] {
                    userJson});
        return ((bool)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginnewUser(string userJson, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("newUser", new object[] {
                    userJson}, callback, asyncState);
    }
    
    /// <remarks/>
    public bool EndnewUser(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((bool)(results[0]));
    }
    
    /// <remarks/>
    public void newUserAsync(string userJson) {
        this.newUserAsync(userJson, null);
    }
    
    /// <remarks/>
    public void newUserAsync(string userJson, object userState) {
        if ((this.newUserOperationCompleted == null)) {
            this.newUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnnewUserOperationCompleted);
        }
        this.InvokeAsync("newUser", new object[] {
                    userJson}, this.newUserOperationCompleted, userState);
    }
    
    private void OnnewUserOperationCompleted(object arg) {
        if ((this.newUserCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.newUserCompleted(this, new newUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/updateUser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public bool updateUser(string userJson) {
        object[] results = this.Invoke("updateUser", new object[] {
                    userJson});
        return ((bool)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginupdateUser(string userJson, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("updateUser", new object[] {
                    userJson}, callback, asyncState);
    }
    
    /// <remarks/>
    public bool EndupdateUser(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((bool)(results[0]));
    }
    
    /// <remarks/>
    public void updateUserAsync(string userJson) {
        this.updateUserAsync(userJson, null);
    }
    
    /// <remarks/>
    public void updateUserAsync(string userJson, object userState) {
        if ((this.updateUserOperationCompleted == null)) {
            this.updateUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateUserOperationCompleted);
        }
        this.InvokeAsync("updateUser", new object[] {
                    userJson}, this.updateUserOperationCompleted, userState);
    }
    
    private void OnupdateUserOperationCompleted(object arg) {
        if ((this.updateUserCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.updateUserCompleted(this, new updateUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void tryLoginCompletedEventHandler(object sender, tryLoginCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class tryLoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal tryLoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public bool Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((bool)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void getUserCompletedEventHandler(object sender, getUserCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class getUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal getUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void newUserCompletedEventHandler(object sender, newUserCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class newUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal newUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public bool Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((bool)(this.results[0]));
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void updateUserCompletedEventHandler(object sender, updateUserCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class updateUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal updateUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public bool Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((bool)(this.results[0]));
        }
    }
}
