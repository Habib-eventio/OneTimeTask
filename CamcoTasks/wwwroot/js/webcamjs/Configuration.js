function ready() {
	if (document.readyState == 'complete') {
		Webcam.set({
			width: 640,
			height: 480,
			image_format: 'jpeg',
			jpeg_quality: 100
		});
		try {
			Webcam.attach('#camera');
		} catch (e) {
			alert(e);
		}
	}
}

function take_snapshot() {
	console.log("in");
	var data = null;
	Webcam.snap(function (data_uri) {
		data = data_uri;
	});
	return data;
}

function chunkString(str, length) {
	var dd = str.match(new RegExp('.{1,' + length + '}', 'g'));
	console.log(dd);
	return str.match(new RegExp('.{1,' + length + '}', 'g'));
}