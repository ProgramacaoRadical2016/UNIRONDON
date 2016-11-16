<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ClinVitta</title>
     <link rel = "shortcut icon" type = "image / x-icon" href ="favicon.ico"/>
    <style type="text/css">
        html, body {
            height: 100%;
            overflow: auto;
        }

        body {
            padding: 0;
            margin: 0;
        }

        #silverlightControlHost {
            height: 100%;
            text-align: center;
        }
    </style>

    <script type="text/javascript" src="Silverlight.js"></script>
    <script type="text/javascript">

        window.onbeforeunload = function () {
            return ' O sistema NewJUR está aberto e pode interromper o processo atual, você tem certeza que deseja sair?';
        };

        window.onunload = function () {
            
        }

        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }

        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-76357120-2', 'auto');
        ga('send', 'pageview');
    </script>
</head>
<body>
    <form id="form1" runat="server" style="height: 100%">
        <div id="silverlightControlHost">
            <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
                <param name="source" value="ClientBin/ClinVitta.xap?versao=<% Response.Write(System.Configuration.ConfigurationManager.AppSettings["versao"].ToString()); %>" />
                <param name="culture" value="pt-BR" />
                <param name="uiculture" value="pt-BR" />
                <param name="onError" value="onSilverlightError" />
                <%--<%
                    string param = "<param name=\"initParams\" value=\"versao=" + System.Configuration.ConfigurationManager.AppSettings["versao"].ToString();
                    if (System.Configuration.ConfigurationManager.AppSettings["guarda_ip"].ToString() == "1")
                        param += ",UserIP=" + Request.UserHostAddress;
                    param += ",Url=" + System.Configuration.ConfigurationManager.AppSettings["url"].ToString();
                    param += "\" />";
                    Response.Write(param);
                %>--%>
                <param name="background" value="white" />
                <param name="minRuntimeVersion" value="5.0.61118.0" />
                <param name="autoUpgrade" value="true" />
                <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0" style="text-decoration: none">
                    <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style: none" />
                </a>
            </object>

            <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px; border: 0px"></iframe>
        </div>

    </form>
</body>
</html>
