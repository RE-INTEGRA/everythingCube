var SerialPort = require("serialport");
var scraper = require('json-scrape')();

SerialPort.list( function (err, ports) { console.log(ports); });

var arduino = new SerialPort.SerialPort('COM5', {baudrate: 9600}); //you must set the port and baudrate

arduino.on('data', function (indata) {
    //console.log(indata.toString());
    scraper.write(indata.toString());  
});

scraper.on('data', function (cleandata) {
    console.log(cleandata);
    //console.log(cleandata.A0);
});

arduino.write( {"led": "1"});