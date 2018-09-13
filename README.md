Unit3D 配置表解决方案
===========

#配置表解决方案说明

系统：

		windows

语言：

		C# + windows批处理BAT

表格：

		Excel 97-2003

驱动：

		1，Excel软件自带驱动。

		2，NPOI开源驱动

自动化脚本：

		windows批处理BAT脚本

配置表说明：

		_TableConfig.xls 为数据配置表指向规则

		_TextConfig.xls 为文本配置表指向规则

		Achievement.xls 为导出数据配置表样例

		text.xls 为导出文本配置表样例

自动化脚本说明：

		makeTable.bat 为Excel软件自带驱动处理脚本，一键处理

		makeTableNPOI.bat 为NPOI开源驱动处理脚本，一键处理

		同理

		exportData.bat，exportText.bat，为Excel自带驱动处理脚本

		exportDataNPOI.bat，exportTextNPOI.bat，为NPOI开源驱动处理脚本，一键处理

脚本说明：

		TextManager.cs 为文本管理类

		RawTable.cs 为读取配置表类

		ClientTableDefine.cs 为配置表数据定义类

		UI_Text.cs 为ugui中Text组件与多语言的衔接组件

		UI_TextEditor.cs 为ugui中多语言的编辑器类

		DataDefine.cs 为数据表配置类的列名映射

		TextDefine.cs 为多语言配置表的文本映射

Welcome to my blog : http://www.luzexi.com <br>
