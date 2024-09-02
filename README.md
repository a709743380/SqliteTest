加入加解密

發佈core於IIS

Web.config：
modules="AspNetCoreModuleV2" 要安裝

且

<aspNetCore  .....  >
<!-- 要添加的內容-->
	<environmentVariables >
					<environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Development"/> 
				</environmentVariables>
    <!-- 要添加的內容-->
</aspNetCore>
