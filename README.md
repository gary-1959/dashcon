# DASHCON Dash File Converter
Reads FutureNET DASH format files and converts to PDF.

<p align="center">
<img src="https://raw.githubusercontent.com/gary-1959/dashcon/main/images/futurenet-1.png" alt="FutureNET DASH Logo" title="FutureNET DASH Logo">
</p>

## Overview
Here are all the source files for a project I did some time ago for viewing and exporting to PDF files created using the old DOS circuit design program FutureNET DASH.

It is written in C# for a Windows PC.

The file DASHCON.zip contains a downloadable installer so might be worth a try if you don't fancy rebuilding the project. 

The file DASH.zip is the FutureNET DASH program which can be made to work on DOSBOX.

If it is of any use, I'll try to answer any questions.

## FutureNet DASH

FutureNet DASH was a schematic capture program written for IBM DOS PCs released in the early 1980s

<p align="center">
<img src="https://raw.githubusercontent.com/gary-1959/dashcon/main/images/dash-on-dosbox.png" alt="DASH Running on DOSBox/Windows 7" title="DASH Running on DOSBox/Windows 7"></p>

<p align="center">DASH Running on DOSBox/Windows 7</p>


It was an extremely expensive package - over $5000 at the time - and was written in assembler. After passing through various hands the software was released as version 6.10 in a de-restricted version, and we have made it available here as a download. You will need a DOS-based machine to run it, though we found it works on <a href = "http://www.dosbox.com/download.php?main=1" target="_blank">DOSBox</a>  quite well.

One thing you will notice is how small a screen of 800 x 600 looks, which is what DASH used to work in, but DOSBox allows you to enlarge the screen size. Refer to the DOSBox web site and help pages for more information.

<p align="center">
<img src="https://raw.githubusercontent.com/gary-1959/dashcon/main/images/dashcon-example.png" alt="DASHCON Displaying Same File" title="DASHCON Displaying Same File"></p>

<p align="center">DASHCON Displaying Same File</p>

## DASHCON File Viewer and Converter
				
I have made a simple Windows program which can be used to view DASH .DWG format files. It will also save drawings as PNG images or PDF files. This software is offered as-is, with no guarantees or support, but it has been tested on Windows 10, Windows 7 and XP machines and will require .Net 3.5 or greater. Let me know if it works for you.




