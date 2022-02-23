<%
Sayfa = "<TABLE border=1 width=500 bgcolor=#E8F0E8 >"
for each x in request.form
Sayfa = Sayfa & "<TR ><TD>" & x & ":</TD><TD>" & request.form(x)  & " </TD></TR>"
next
Sayfa = Sayfa & "</TABLE>"
%>
<%
Dim MAIL
Set MAIL = Server.CreateObject("Persits.MailSender")
MAIL.Host = "srvm11.trwww.com"
MAIL.From = "test@palarchive.info" 'bizden aldnz mail olmal
MAIL.Username = "test@palarchive.info" 
MAIL.Password = "4JfwJ97L" ' e-Mailinizin ifresini giriniz
MAIL.FromName = "Persits"
MAIL.AddAddress "dturhost@gmail.com"
MAIL.Subject = "Persits"
MAIL.IsHTML = True
MAIL.Body = "Persits komponentini kullanarak site zerinden mail gnderme"
MAIL.Send

If err Then ' hata mesajn alalm Mail Gnderilmemise..
Response.Write err.Description & "<br>Mesajnz Gnderilmedi.."
Else ' Mail Gnderilmi ise
Response.Write("<script>alert('lginize Teekkr Ederiz..');location.href='mesaj.htm';</script>" )
End If


set MAIL = nothing
%>
