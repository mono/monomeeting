<%@ Page Language="C#" Src="base.cs" Inherits="Meeting.MyBase" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Mail" %>
<html>
<head>
<link rel="stylesheet" type="text/css" href="meeting.css"/>
</head>
<body>
<div id="globalWrapper">
    <div id="bigWrapper">
        <div class="portlet" id="p-logo">
	  <a href="/index.aspx" title="Main Page"></a>
	</div>
        <div class="portlet" id="p-nav">
	  <div style="font-weight: bold">Mono User and Developers Meeting</div>
	  <div style="font-size:9pt">23-24 October 2006;   Cambridge, MA, USA.</div>
        </div>
     </div>

<div id="menu">
  <ul>
    <li><a href="index.aspx">Main Page</a>
    <li><a href="schedule.aspx">Talks/Schedule</a>
    <li><a href="registration.aspx">Registration</a>
    <li><a href="index.aspx#location">Location</a>
    <li><a href="http://mono.wiki.com/2006MonoMeeting">Event Wiki</a> 
    <li><a href="http://mono.wiki.com/2006_Mono-Meeting/Traveling_to_Boston">Traveling Tips</a>
    <li><a href="http://mono.wiki.com/2006MonoMeeting/Sponsorships">Sponsorship</a>
  </ul>
</div>
	
