<?xml version="1.0" encoding="ISO-8859-1" ?>
<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"
	xmlns:f="http://java.sun.com/jsf/core"
	xmlns:h="http://java.sun.com/jsf/html"
	xmlns:p="http://primefaces.org/ui">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
<title>Insert 1 here</title>
</head>
<body>
  <h:form id="form">
    <p:panel id="panel" header="Form" style="margin-bottom:10px;">
        <p:messages id="messages" />
        <h:panelGrid columns="3" cellpadding="5">
            <p:outputLabel for="text1" value="Text 1:" />
            <p:inputText id="text1" value="#{resetInputView.text1}" required="true" label="Firstname">
                <f:validateLength minimum="2" />
            </p:inputText>
            <p:message for="text1" />
 
            <p:outputLabel for="text2" value="Text 2:" />
            <p:inputText id="text2" value="#{resetInputView.text2}" required="true" label="Surname"/>
            <p:message for="text2" />
        </h:panelGrid>
    </p:panel>
 
    <p:toolbar>
        <f:facet name="left">
            <p:commandButton value="Submit" update="panel" actionListener="#{resetInputView.save}" style="margin-right:20px;" />
        </f:facet>
 
        <f:facet name="right">
            <p:commandButton value="Reset Fail" update="panel" process="@this" actionListener="#{resetInputView.resetFail}" style="margin-right:20px;" />
 
            <p:commandButton value="Reset Tag" update="panel" process="@this" style="margin-right:20px;" >
                <p:resetInput target="panel" />
            </p:commandButton>
 
            <p:commandButton value="Reset Code" update="panel" process="@this" actionListener="#{resetInputView.reset}"  style="margin-right:20px;" />
 
            <p:commandButton value="Reset Non-Ajax" actionListener="#{resetInputView.reset}" immediate="true" ajax="false" style="margin-right:20px;">
                <p:resetInput target="panel" />
            </p:commandButton>
 
            <h:commandButton value="Reset p:ajax" style="margin-right:20px;" >
                <p:ajax update="panel" resetValues="true" />
            </h:commandButton>
        </f:facet>
    </p:toolbar>
</h:form>
</body>
</html>