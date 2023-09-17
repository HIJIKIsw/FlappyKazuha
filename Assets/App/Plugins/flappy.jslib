mergeInto(LibraryManager.library, {
	PromptInternal: function (message, defaultText) {
		var defaultTextConverted = UTF8ToString(defaultText);
		var returnStr = prompt(UTF8ToString(message), defaultTextConverted);
		if( returnStr == null || returnStr == "" )
		{
			returnStr = "%canceled%";
		}
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;
	},
});
