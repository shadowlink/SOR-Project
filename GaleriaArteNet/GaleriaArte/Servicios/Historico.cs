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
// This source code was auto-generated by wsdl, Version=4.0.30319.33440.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="HistoricoSoap", Namespace="http://tempuri.org/")]
public partial class Historico : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback devolverTodoOperationCompleted;
    
    /// <remarks/>
    public Historico() {
        this.Url = "http://192.168.1.16:6543/Historico.asmx";
    }
    
    /// <remarks/>
    public event devolverTodoCompletedEventHandler devolverTodoCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/devolverTodo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public string devolverTodo(int userId) {
        object[] results = this.Invoke("devolverTodo", new object[] {
                    userId});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BegindevolverTodo(int userId, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("devolverTodo", new object[] {
                    userId}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EnddevolverTodo(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void devolverTodoAsync(int userId) {
        this.devolverTodoAsync(userId, null);
    }
    
    /// <remarks/>
    public void devolverTodoAsync(int userId, object userState) {
        if ((this.devolverTodoOperationCompleted == null)) {
            this.devolverTodoOperationCompleted = new System.Threading.SendOrPostCallback(this.OndevolverTodoOperationCompleted);
        }
        this.InvokeAsync("devolverTodo", new object[] {
                    userId}, this.devolverTodoOperationCompleted, userState);
    }
    
    private void OndevolverTodoOperationCompleted(object arg) {
        if ((this.devolverTodoCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.devolverTodoCompleted(this, new devolverTodoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
public delegate void devolverTodoCompletedEventHandler(object sender, devolverTodoCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class devolverTodoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal devolverTodoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
