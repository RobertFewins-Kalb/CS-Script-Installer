Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Security.AccessControl
Imports Microsoft.Win32

Public Class scripts_config_form

    Const read_only = 1
    Const read_write = 2



    Private Shared Sub Get45or451FromRegistry(dot_net_from_registry)

    End Sub

    Private Shared Function CheckFor45DotVersion(releaseKey As Integer) As String
        If (releaseKey >= 379893) Then
            Return "4.5.2 or later"
        End If
        If (releaseKey >= 378675) Then
            Return "4.5.1 or later"
        End If
        If (releaseKey >= 378389) Then
            Return "4.5 or later"
        End If
        ' This line should never execute. A non-null release key should mean 
        ' that 4.5 or later is installed. 
        Return "No 4.5 or later version detected"
    End Function

    Private Property list_of_addresses As String

    Private Property fso As Object

    Private Property FSO_new_file_path As Object

    Private Property ObjFSO As Object

    Private Property objFile As Object

    Private Property function_file_lines As Object

    Private Property line_to_look_for_in_functions_file As String

    Private Property warning As MsgBoxResult

    Private Property list_of_files_array As String()

    Private Property text_file As String

    Private Property new_text_file As Object

    Private Property GitHub_current_zip_archive As String

    Private Property objXMLHTTP As Object

    Private Property objADOStream As Object

    Private Property objShell As Object

    Private Property old_file_path As Object




    ''This is the function that actually modifies the files
    'Function update_files(file_name, address_array)
    '
    
    'End Function




    '    Sub downloading_files_from_GitHub()
    '        Dim agency_is_beta As Boolean
    '
    '        'Only some agencies get the option to install beta scripts, based mainly on their status as contributing agencies (they write scripts).
    '        'As a result, we have an if/then list. It will need to be updated frequently as agencies join/leave the beta program.
    '        If county_selection.Text = "02 - Anoka County" Or _
    '            county_selection.Text = "05 - Benton County" Or _
    '            county_selection.Text = "19 - Dakota County" Or _
    '            county_selection.Text = "20 - Dodge County" Or _
    '            county_selection.Text = "55 - Olmsted County" Or _
    '            county_selection.Text = "57 - Pennington County" Or _
    '            county_selection.Text = "69 - St. Louis County" Or _
    '            county_selection.Text = "73 - Stearns County" Or _
    '            county_selection.Text = "74 - Steele County" Or _
    '            county_selection.Text = "79 - Wabasha County" Then
    '
    '            Dim beta_msgbox As String
    '            beta_msgbox = MsgBox("Your agency is listed as a beta agency. Install beta versions of scripts?", MsgBoxStyle.YesNoCancel)
    '            If beta_msgbox = 2 Then Exit Sub
    '            If beta_msgbox = 6 Then agency_is_beta = True
    '            If beta_msgbox = 7 Then agency_is_beta = False
    '
    '        End If
    '
    '        'Variables for local_copy_of_zip_file and temp_folder are used by this sub. These variables are temporary.
    '        Dim local_copy_of_zip_file = location_to_save_script_files.Text & "\temp\master.zip"
    '        Dim temp_folder = location_to_save_script_files.Text & "\temp"
    '
    '        'If the agency is a beta agency, they'll have the beta version of the scripts instead of the main one.
    '        If agency_is_beta = True Then
    '            GitHub_current_zip_archive = "https://github.com/MN-Script-Team/DHS-MAXIS-Scripts/archive/beta.zip"
    '        Else
    '            GitHub_current_zip_archive = "https://github.com/MN-Script-Team/DHS-MAXIS-Scripts/archive/Release.zip"
    '        End If
    '
    '        'First, we create a temp directory for all this madness.
    '        fso = CreateObject("Scripting.FileSystemObject")
    '        If fso.folderexists(temp_folder) = True Then fso.deletefolder(temp_folder) 'Clears the temp folder if it exists.
    '        fso.CreateFolder(temp_folder)
    '
    '        '---------------------------------------------------------------------------------------
    '        'Now it downloads the zip file from Github. This code was copied from https://gist.github.com/udawtr/2053179 on 09/13/2014, and modified for our purposes.
    '
    '        'Creating a server object
    '        objXMLHTTP = CreateObject("MSXML2.ServerXMLHTTP")
    '
    '        'Opening the file
    '        objXMLHTTP.open("GET", GitHub_current_zip_archive, False)
    '        objXMLHTTP.send()
    '        If objXMLHTTP.Status = 200 Then     'Guessing this means "found" but admittedly I'm not sure. -VKC, 09/13/2014
    '            objADOStream = CreateObject("ADODB.Stream")
    '            objADOStream.Open()
    '            objADOStream.Type = 1 'adTypeBinary
    '            objADOStream.Write(objXMLHTTP.ResponseBody)
    '            objADOStream.Position = 0    'Set the stream position to the start
    '
    '            'Writing the file to the hard disk
    '            ObjFSO = CreateObject("Scripting.FileSystemObject")
    '            If ObjFSO.Fileexists(local_copy_of_zip_file) Then ObjFSO.DeleteFile(local_copy_of_zip_file)
    '            ObjFSO = Nothing
    '            objADOStream.SaveToFile(local_copy_of_zip_file)
    '            objADOStream.Close()
    '            objADOStream = Nothing
    '        End If
    '
    '        objXMLHTTP = Nothing
    '        '------------------------------------------------------------------------------------
    '        'Now, because the ZipFile.ExtractToDirectory method appears to not work with folks running .net 4.0, the
    '        'app will create a VBS version of the file extractor, and save it to disk.
    '
    '        'Creating the file and writing each line
    '        Dim new_zip_file As StreamWriter = File.CreateText(temp_folder & "\unzip.vbs")
    '        new_zip_file.WriteLine("current_directory = " & Chr(34) & temp_folder & Chr(34))                                                        'Sets current directory to match temp folder
    '        new_zip_file.WriteLine("set objShell = CreateObject(" & Chr(34) & "Shell.Application" & Chr(34) & ") ")                                 'Creates a shell.application object to execute extraction
    '        new_zip_file.WriteLine("set FilesInZip = objShell.NameSpace(current_directory & " & Chr(34) & "\master.zip" & Chr(34) & ").items")      'Points to the master.zip file
    '        new_zip_file.WriteLine("objShell.NameSpace(current_directory).CopyHere FilesInZip, 16 ")                                                'Copies the files out to the temp directory
    '        new_zip_file.WriteLine("set objShell = Nothing")                                                                                        'Nulls variable like a good little code
    '        new_zip_file.Flush()
    '        new_zip_file.Close()
    '
    '        new_zip_file = Nothing  'clears variable
    '
    '
    '        'Running the zip file. Calls wscript, as a surprising number of folks have somehow modified their default program for scripts to be notepad.
    '        'Apparently people want to hack scripts to do their own bidding. Who'd have thought?
    '        Process.Start("wscript", Chr(34) & temp_folder & "\unzip.vbs" & Chr(34))
    '
    '        'Now, because we have an independent script running, we need to check to see if wscript.exe is running. So, we set a variable to be our boolean "are we there yet"?
    '        Dim moving_on As Boolean
    '
    '        'Now it loops checking to see if wscript.exe is running. If it STOPS running, moving_on is set to true, and we continue on our merry way.
    '        Do
    '            Dim processes() As Process
    '            processes = Process.GetProcessesByName("wscript")
    '            If processes.Count > 0 Then
    '                moving_on = False
    '            Else
    '                moving_on = True
    '            End If
    '        Loop Until moving_on = True
    '
    '        'There should only be one folder in the download, but the directory checker creates an array. We need to join the array before we can mess with it.
    '        Dim dirs As List(Of String) = New List(Of String)(Directory.EnumerateDirectories(temp_folder))
    '        Dim github_folder As String = String.Join(",", dirs)
    '
    '        'Now we move the folder to its new home.
    '        fso.CopyFolder(github_folder & "\Script Files", location_to_save_script_files.Text & "\Script Files", True)
    '
    '        'Now we delete the temp directory.
    '        fso.DeleteFolder(temp_folder)
    '    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim frmAbout As New AboutBox
        frmAbout.ShowDialog(Me)
    End Sub

    Private Sub EDMS_choice_TextClear(ByVal sender As Object, ByVal e As System.EventArgs) Handles EDMS_choice.Enter
        EDMS_choice.Text = ""
    End Sub

    Private Sub config_button_Click(sender As Object, e As EventArgs) Handles run_configuration_button.Click
        'Dims the address array as nothing
        Dim list_of_addresses = Nothing

        'Takes each line of the address array and splits it with a "|", and each address with a "~". FUNCTIONS FILE will take it from here and split it.
        If address_entry_form.addr_01_line_01.Text <> "" And address_entry_form.addr_01_line_02.Text <> "" Then list_of_addresses = list_of_addresses & address_entry_form.addr_01_line_01.Text & "|" & address_entry_form.addr_01_line_02.Text & "~"
        If address_entry_form.addr_02_line_01.Text <> "" And address_entry_form.addr_02_line_02.Text <> "" Then list_of_addresses = list_of_addresses & address_entry_form.addr_02_line_01.Text & "|" & address_entry_form.addr_02_line_02.Text & "~"
        If address_entry_form.addr_03_line_01.Text <> "" And address_entry_form.addr_03_line_02.Text <> "" Then list_of_addresses = list_of_addresses & address_entry_form.addr_03_line_01.Text & "|" & address_entry_form.addr_03_line_02.Text & "~"
        If address_entry_form.addr_04_line_01.Text <> "" And address_entry_form.addr_04_line_02.Text <> "" Then list_of_addresses = list_of_addresses & address_entry_form.addr_04_line_01.Text & "|" & address_entry_form.addr_04_line_02.Text & "~"
        If address_entry_form.addr_05_line_01.Text <> "" And address_entry_form.addr_05_line_02.Text <> "" Then list_of_addresses = list_of_addresses & address_entry_form.addr_05_line_01.Text & "|" & address_entry_form.addr_05_line_02.Text & "~"
        If address_entry_form.addr_06_line_01.Text <> "" And address_entry_form.addr_06_line_02.Text <> "" Then list_of_addresses = list_of_addresses & address_entry_form.addr_06_line_01.Text & "|" & address_entry_form.addr_06_line_02.Text & "~"
        If address_entry_form.addr_07_line_01.Text <> "" And address_entry_form.addr_07_line_02.Text <> "" Then list_of_addresses = list_of_addresses & address_entry_form.addr_07_line_01.Text & "|" & address_entry_form.addr_07_line_02.Text & "~"

        'Warning if a county or file path is not selected
        If county_selection.Text = "" Or list_of_addresses = Nothing Or location_to_save_script_files.Text = "" Then
            MsgBox("You must select a county, and enter a complete county address, as well as enter a file location.")
            Exit Sub
        End If

        'Removing the last tilde as it is stupid and not required.
        list_of_addresses = list_of_addresses.Remove(list_of_addresses.Length - 1)

        'Disabling features/enabling others (more of a pretty GUI setup, might rewrite to be even prettier)
        Update_Files_Label.Visible = True
        Tab_Control_Main_Form.Enabled = False
        run_configuration_button.Enabled = False

        'Setting EDMS_choice as DHS eDocs if there is not a local EDMS.
        If EDMS_check.Checked = False Then EDMS_choice.Text = "DHS eDocs"

        'LOADING TEXT FILE OF SCRIPTS TO MAKE FROM GITHUB-----------------------------------------------------------------------
        Dim redirect_list   'We'll need this in a bit
        Dim url = "https://raw.githubusercontent.com/Anoka-Script-Team/Anoka-PRISM-Scripts/master/Script%20Files/LIST%20OF%20SCRIPTS.txt"
        Dim req = CreateObject("Msxml2.XMLHttp.6.0")            'Creates an object to get a URL
        req.open("GET", url, False)                             'Attempts to open the URL
        req.send()                                              'Sends request
        If req.Status = 200 Then                                '200 means great success
            fso = CreateObject("Scripting.FileSystemObject")    'Creates an FSO
            redirect_list = req.responseText                    'Loads the list of scripts
        End If
        redirect_list = Split(redirect_list, vbLf)               'Splits redirect list into an array to be used by the next section

        'CREATING ALL OF THE REDIRECTS------------------------------------------------------------------------------------------

        'Declaring variables needed for creating all of these new text files.
        Dim create_VBS_fso
        Dim create_VBS_command

        'Creating each redirect from the script list text file
        For Each redirect_to_make In redirect_list

            'Determines which folder ant title the redirect requires
            Dim redirect_path As String
            If InStr(redirect_to_make, "REDIRECT - ACTIONS") Then
                redirect_path = "ACTIONS/ACTIONS - MAIN MENU.vbs"
            ElseIf InStr(redirect_to_make, "REDIRECT - NOTES") Then
                redirect_path = "NOTES/NOTES - MAIN MENU.vbs"
            ElseIf InStr(redirect_to_make, "REDIRECT - NAV") Then
                redirect_path = "NAV/" & Replace(redirect_to_make, "REDIRECT - ", "")
            End If

            'Here's the file contents for each one
            Dim redirect_file_contents = "'LOADING GLOBAL VARIABLES--------------------------------------------------------------------" & vbCr & _
                "Set run_another_script_fso = CreateObject(""Scripting.FileSystemObject"")" & vbCr & _
                "Set fso_command = run_another_script_fso.OpenTextFile(""" & location_to_save_script_files.Text & "\Script Files\SETTINGS - GLOBAL VARIABLES.vbs"")" & vbCr & _
                "text_from_the_other_script = fso_command.ReadAll" & vbCr & _
                "fso_command.Close" & vbCr & _
                "Execute text_from_the_other_script" & vbCr & _
                vbCr & _
                "'LOADING SCRIPT" & vbCr & _
                "url = script_repository & ""/" & redirect_path & """" & vbCr & _
                "SET req = CreateObject(""Msxml2.XMLHttp.6.0"")				'Creates an object to get a URL" & vbCr & _
                "req.open ""GET"", url, FALSE									'Attempts to open the URL" & vbCr & _
                "req.send													'Sends request" & vbCr & _
                "IF req.Status = 200 THEN									'200 means great success" & vbCr & _
                "   Set fso = CreateObject(""Scripting.FileSystemObject"")	'Creates an FSO" & vbCr & _
                "	Execute req.responseText								'Executes the script code" & vbCr & _
                "ELSE														'Error message, tells user to try to reach github.com, otherwise instructs to contact Veronica with details (and stops script)." & vbCr & _
                "	MsgBox 	""Something has gone wrong. The code stored on GitHub was not able to be reached."" & vbCr &_ " & vbCr & _
                "	vbCr & _" & vbCr & _
                "		""Before contacting Veronica Cary, please check to make sure you can load the main page at www.GitHub.com."" & vbCr &_" & vbCr & _
                "		vbCr & _" & vbCr & _
                "		""If you can reach GitHub.com, but this script still does not work, ask an alpha user to contact Veronica Cary and provide the following information:"" & vbCr &_" & vbCr & _
                "		vbTab & ""- The name of the script you are running."" & vbCr &_" & vbCr & _
                "		vbTab & ""- Whether or not the script is """"erroring out"""" for any other users."" & vbCr &_" & vbCr & _
                "		vbTab & ""- The name and email for an employee from your IT department,"" & vbCr & _" & vbCr & _
                "		vbTab & vbTab & ""responsible for network issues."" & vbCr &_" & vbCr & _
                "		vbTab & ""- The URL indicated below (a screenshot should suffice)."" & vbCr &_" & vbCr & _
                "		vbCr & _" & vbCr & _
                "		""Veronica will work with your IT department to try and solve this issue, if needed."" & vbCr &_ " & vbCr & _
                "		vbCr &_" & vbCr & _
                "		""URL: "" & url" & vbCr & _
                "		StopScript" & vbCr & _
                "END IF"

            'Creates each script file one at a time
            create_VBS_fso = CreateObject("Scripting.FileSystemObject")
            If create_VBS_fso.FolderExists(location_to_save_script_files.Text & "/Script files/") = False Then create_VBS_fso.CreateFolder(location_to_save_script_files.Text & "/Script files/")
            create_VBS_command = create_VBS_fso.CreateTextFile(location_to_save_script_files.Text & "/Script files/" & Trim(redirect_to_make), 2)
            create_VBS_command.Write(redirect_file_contents)
            create_VBS_command.Close()
            create_VBS_fso = Nothing
        Next

        'DOWNLOADING THE POWER PAD----------------------------------------------------------------------------------------------

        'URL for Power Pad (eventually this should be dynamic dependent on the beta/master/other categories, but I need to get this out soon).
        Dim pad_URL = "https://github.com/Anoka-Script-Team/Anoka-PRISM-Scripts/blob/master/Script%20Files/PAD%20-%20CS.pad?raw=true"

        'Downloads file
        'Now it downloads the pad file from Github. This code was copied from https://gist.github.com/udawtr/2053179 on 09/13/2014, and modified for our purposes.

        'Creating a server object
        objXMLHTTP = CreateObject("MSXML2.ServerXMLHTTP")

        'Opening the file
        objXMLHTTP.open("GET", pad_URL, False)
        objXMLHTTP.send()
        If objXMLHTTP.Status = 200 Then
            objADOStream = CreateObject("ADODB.Stream")
            objADOStream.Open()
            objADOStream.Type = 1 'adTypeBinary
            objADOStream.Write(objXMLHTTP.ResponseBody)
            objADOStream.Position = 0    'Set the stream position to the start

            'Writing the file to the hard disk
            ObjFSO = CreateObject("Scripting.FileSystemObject")
            If ObjFSO.Fileexists(location_to_save_script_files.Text & "\Script Files\PAD - CS.pad") Then ObjFSO.DeleteFile(location_to_save_script_files.Text & "\Script Files\PAD - CS.pad")
            ObjFSO = Nothing
            objADOStream.SaveToFile(location_to_save_script_files.Text & "\Script Files\PAD - CS.pad")
            objADOStream.Close()
            objADOStream = Nothing
        End If

        objXMLHTTP = Nothing


        'DOWNLOADING THE GLOBAL VARIABLES FILE AND CONFIGURING IT---------------------------------------------------------------

        'URL for Global Variables (eventually this should be dynamic dependent on the beta/master/other categories, but I need to get this out soon).
        Dim global_variables_URL = "https://raw.githubusercontent.com/Anoka-Script-Team/Anoka-PRISM-Scripts/master/Script%20Files/SETTINGS%20-%20GLOBAL%20VARIABLES.vbs"

        'Checks if the file exists. If it does, it deletes it.
        If File.Exists(location_to_save_script_files.Text & "\Script Files\SETTINGS - GLOBAL VARIABLES.vbs") Then File.Delete(location_to_save_script_files.Text & "\Script Files\SETTINGS - GLOBAL VARIABLES.vbs")

        'Downloads file
        My.Computer.Network.DownloadFile(global_variables_URL, location_to_save_script_files.Text & "\Script Files\SETTINGS - GLOBAL VARIABLES.vbs")

        'Opening file read-only, to know what path to overwrite from the GitHub repo.
        ObjFSO = CreateObject("Scripting.FileSystemObject")
        objFile = ObjFSO.OpenTextFile(location_to_save_script_files.Text & "\Script Files\SETTINGS - GLOBAL VARIABLES.vbs", read_only)

        'Dim-ing variables needed for the following do...loop
        Dim global_variables_lines
        Dim line_to_look_for_in_global_variables

        'Reading each line, seeking the old path name from the GitHub repo.
        Do Until objFile.AtEndOfStream
            global_variables_lines = objFile.ReadLine
            line_to_look_for_in_global_variables = "'Set fso_command = run_another_script_fso.OpenTextFile("
            If InStr(global_variables_lines, line_to_look_for_in_global_variables) Then
                old_file_path = Replace(Replace(Replace(global_variables_lines, line_to_look_for_in_global_variables, ""), Chr(34), ""), "Script Files\SETTINGS - GLOBAL VARIABLES.vbs)", "")
            End If
        Loop

        'Closing the read-only version
        objFile.Close()

        'Reading all lines from it into a string
        Dim text_file() As String = System.IO.File.ReadAllLines(location_to_save_script_files.Text & "\Script Files\SETTINGS - GLOBAL VARIABLES.vbs")
        Dim text_line As String

        'Replaces file path and county-specific variables
        For Each text_line In text_file
            If custom_file_path.Text = Nothing Then
                text_line = Replace(text_line, old_file_path, location_to_save_script_files.Text & "\")
            Else
                text_line = Replace(text_line, old_file_path, custom_file_path.Text & "\")
            End If

            If InStr(text_line, "EDMS_choice = ") Then text_line = "EDMS_choice = " & Chr(34) & EDMS_choice.Text & Chr(34)
            If InStr(text_line, "county_office_array = split(") Then text_line = "county_office_array = split(" & Chr(34) & list_of_addresses & Chr(34) & ", " & Chr(34) & "~" & Chr(34) & ")"
            If InStr(text_line, "county_name = ") Then text_line = "county_name = " & Chr(34) & county_selection.Text & Chr(34)

            'INSERT COLLECTING STATS FIXES HERE WHEN ACCESS GOES LIVE
            new_text_file = new_text_file & text_line & Chr(10)
        Next

        'Splits the array created by the For...next and writes each line into the file
        new_text_file = Split(new_text_file, Chr(10))
        System.IO.File.WriteAllLines(location_to_save_script_files.Text & "\Script Files\SETTINGS - GLOBAL VARIABLES.vbs", new_text_file)

        'Switches controls back to default
        Update_Files_Label.Visible = False
        Tab_Control_Main_Form.Enabled = True
        run_configuration_button.Enabled = True

        'Success!
        Me.Hide()
        MsgBox("Success! Child Support BlueZone Scripts installed in this directory.")
        Application.Exit()
    End Sub

    Private Sub FileOpen(Optional p1 As Object = Nothing, Optional file As Object = Nothing, Optional openMode As OpenMode = Nothing, Optional openAccess As OpenAccess = Nothing, Optional p5 As Object = Nothing, Optional p6 As Object = Nothing)
        Throw New NotImplementedException
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        address_entry_form.ShowDialog(Me)

    End Sub


    'This sub finds the current directory, and then offers to copy the existing values from FUNCTIONS FILE. It triggers when the user
    'selects "browse" from the "where are we saving scripts" part of the main tab.
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        FolderBrowserDialog1.ShowDialog()

        'It does the next piece in case the user accidentally sets the path as "Script Files". 
        'This will eliminate the recursive "Script Files\Script Files\Script Files" which is bound to happen with less savvy folks.
        If Strings.Right(FolderBrowserDialog1.SelectedPath, 13) = "\Script Files" Then
            FolderBrowserDialog1.SelectedPath = Strings.Left(FolderBrowserDialog1.SelectedPath, FolderBrowserDialog1.SelectedPath.Length - 13)
        End If

        'If there's script files in the folder, this will find them and ask if we want to try and autofill the dialog.
        If File.Exists(FolderBrowserDialog1.SelectedPath & "/Script Files/SETTINGS - GLOBAL VARIABLES.vbs") = True Then
            Dim files_found_messagebox = MsgBox("This folder contains script files! Would you like the installer to try and read the address(es) and agency for you?", MsgBoxStyle.YesNo)
            If files_found_messagebox = MsgBoxResult.Yes Then
                Dim GLOBAL_VARIABLES_text() As String = File.ReadAllLines(FolderBrowserDialog1.SelectedPath & "/Script Files/SETTINGS - GLOBAL VARIABLES.vbs")
                For Each GLOBAL_VARIABLES_line In GLOBAL_VARIABLES_text
                    If InStr(GLOBAL_VARIABLES_line, "county_name = ") Then
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, "county_name = ", "")
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, Chr(34), "")
                        county_selection.Text = GLOBAL_VARIABLES_line
                    End If

                    If InStr(GLOBAL_VARIABLES_line, "EDMS_choice = ") Then
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, "EDMS_choice = ", "")
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, Chr(34), "")
                        EDMS_choice.Text = GLOBAL_VARIABLES_line
                        If EDMS_choice.Text <> "DHS eDocs" Then EDMS_check.Checked = True
                    End If
                    If InStr(GLOBAL_VARIABLES_line, "'Set fso_command = run_another_script_fso.OpenTextFile(") Then
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, "'Set fso_command = run_another_script_fso.OpenTextFile(", "")
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, "\Script Files\SETTINGS - GLOBAL VARIABLES.vbs", "")
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, ")", "")
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, Chr(34), "")
                        If GLOBAL_VARIABLES_line <> FolderBrowserDialog1.SelectedPath Then
                            custom_file_path.Text = GLOBAL_VARIABLES_line
                        End If
                    End If
                    If InStr(GLOBAL_VARIABLES_line, "county_office_array = split(") Then
                        Dim reading_address_info As Array
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, "county_office_array = split(" & Chr(34), "")
                        GLOBAL_VARIABLES_line = Replace(GLOBAL_VARIABLES_line, Chr(34) & ", " & Chr(34) & "~" & Chr(34) & ")", "")
                        reading_address_info = Split(GLOBAL_VARIABLES_line, "~")
                        For array_number = 0 To UBound(reading_address_info)
                            If array_number = 0 Then
                                Dim line_to_add = Split(reading_address_info(0), "|")
                                address_entry_form.addr_01_line_01.Text = line_to_add(0)
                                address_entry_form.addr_01_line_02.Text = line_to_add(1)
                            End If
                            If array_number = 1 Then
                                Dim line_to_add = Split(reading_address_info(1), "|")
                                address_entry_form.addr_02_line_01.Text = line_to_add(0)
                                address_entry_form.addr_02_line_02.Text = line_to_add(1)
                            End If
                            If array_number = 2 Then
                                Dim line_to_add = Split(reading_address_info(2), "|")
                                address_entry_form.addr_03_line_01.Text = line_to_add(0)
                                address_entry_form.addr_03_line_02.Text = line_to_add(1)
                            End If
                            If array_number = 3 Then
                                Dim line_to_add = Split(reading_address_info(3), "|")
                                address_entry_form.addr_04_line_01.Text = line_to_add(0)
                                address_entry_form.addr_04_line_02.Text = line_to_add(1)
                            End If
                            If array_number = 4 Then
                                Dim line_to_add = Split(reading_address_info(4), "|")
                                address_entry_form.addr_05_line_01.Text = line_to_add(0)
                                address_entry_form.addr_05_line_02.Text = line_to_add(1)
                            End If
                            If array_number = 5 Then
                                Dim line_to_add = Split(reading_address_info(5), "|")
                                address_entry_form.addr_06_line_01.Text = line_to_add(0)
                                address_entry_form.addr_06_line_02.Text = line_to_add(1)
                            End If
                            If array_number = 6 Then
                                Dim line_to_add = Split(reading_address_info(6), "|")
                                address_entry_form.addr_07_line_01.Text = line_to_add(0)
                                address_entry_form.addr_07_line_02.Text = line_to_add(1)
                            End If
                        Next
                    End If
                Next
            End If
        End If

        'Now it updates the text for the file save location.
        location_to_save_script_files.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub custom_file_path_TextChanged(sender As Object, e As EventArgs) Handles custom_file_path.Leave
        'Removing the entry if the user puts a slash as the last character.
        If InStrRev(custom_file_path.Text, "\") = Len(custom_file_path.Text) Then
            MsgBox("You cannot enter a slash as the last character. Your entry will now be cleared.")
            custom_file_path.Text = ""
        End If
    End Sub


    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click


        Dim max_dot_net_version As String
        Using ndpKey As RegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                    RegistryView.Registry32).OpenSubKey("SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\")

            If ndpKey IsNot Nothing AndAlso ndpKey.GetValue("Release") IsNot Nothing Then
                max_dot_net_version = ("Version: " & CheckFor45DotVersion(CInt(ndpKey.GetValue("Release"))))
            Else
                max_dot_net_version = ("Version 4.5 or later is not detected.")
            End If
        End Using

        MsgBox(".NET version currently used: " & Environment.Version.ToString() & vbCr & _
               ".NET 4.5 version installed: " & max_dot_net_version)

    End Sub

End Class
