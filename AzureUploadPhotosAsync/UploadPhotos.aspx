<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPhotos.aspx.cs" Inherits="AzureUploadPhotosAsync.UploadPhotos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function ClientUploadComplete() {
            window.location.replace("MyPhotos.aspx?album=10");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="sriptmanger" runat="server" CombineScripts="false" />
        <div style="width: 500px; margin: 0 auto;">
            <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" AllowedFileTypes="jpg,jpeg,png,gif,tiff"
                MaximumNumberOfFiles="50" OnUploadComplete="File_Upload" OnClientUploadCompleteAll="ClientUploadComplete" ForeColor="Red" />
        </div>
    </form>
</body>
</html>
