<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CadastroDeContatosVS._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <main>
            
        <div>
<h2>Cadastro de Contato</h2>

<label>Nome:</label>
<asp:TextBox ID="NomeTxt" runat="server" />
               <asp:RequiredFieldValidator ID="NomeValidator"
                    ControlToValidate="NomeTxt"
                    Display="Static"
                    ValidationGroup="BotaoCadastrar"
                    ErrorMessage="Campo Nome obrigatório"
                    runat="server"/>
        <br />
        <br />
          
<label>Email:</label>
<asp:TextBox ID="EmailTxt" runat="server" />
            <asp:RequiredFieldValidator ID="EmailValidator"
     ControlToValidate="EmailTxt"
     Display="Static"
     ValidationGroup="BotaoCadastrar"
     ErrorMessage="Campo Email obrigatório"
     runat="server"/>
        <br />
        <br />

<label>Data de Nascimento:</label>
<asp:TextBox ID="DataTxt" runat="server" />
            <asp:RequiredFieldValidator ID="DataValidator"
     ControlToValidate="DataTxt"
     Display="Static"
     ValidationGroup="BotaoCadastrar"
     ErrorMessage="Campo Data de Nascimento obrigatório"
     runat="server"/>
        <br />
        <br />

<label>CPF:</label>
<asp:TextBox ID="CpfTxt" runat="server" />
            <asp:RequiredFieldValidator ID="CPFValidator"
     ControlToValidate="CpfTxt"
     Display="Static"
     ValidationGroup="BotaoCadastrar"
     ErrorMessage="Campo CPF obrigatório"
     runat="server"/>
        <br />
        <br />

<label>Cidade:</label>
<asp:TextBox ID="CidadeTxt" runat="server" />
        <br />
        <br />

<label>Estado:</label>
<asp:TextBox ID="EstadoTxt" runat="server" />
        <br />
        <br />

<label>Endereço:</label>
<asp:TextBox ID="EnderecoTxt" runat="server" />
        <br />
        <br />
          
          <asp:Button ID="Cadastrar" runat="server" OnClick="Cadastrar_Click" Text="Cadastrar" ValidationGroup="BotaoCadastrar"/>
          <asp:Button ID="Limpar" runat="server" OnClick="Limpar_Click" Text="Limpar" />   
            </div> 
          <br />
          <br />
         
           
                <asp:Repeater ID="repeater" runat="server"> 
      <HeaderTemplate>
          <table ID="tabela" border ="1"> 
              <tr>
         <th>Ações</th>
        <th>Nome</th>
        <th>Email</th>
        <th>Data de Nascimento</th>
        <th>CPF</th>
        <th>Cidade</th>
        <th>Estado</th>
        <th>Endereço</th>
                  </tr>
        </HeaderTemplate>
                       
            <ItemTemplate>
                 <tr>
                     <td>
            <asp:Button ID="Editar" runat="server" Text="Editar" CommandArgument='<%# Eval("Id") %>' OnClick="Editar_Click"/>
            <asp:Button ID="Excluir" runat="server" Text="Excluir" CommandArgument='<%# Eval("Id") %>' OnClick ="Excluir_Click"/></td>
            <td><%# Eval("Nome") %></td>
            <td><%# Eval("Email") %></td>
            <td><%# Eval("DataDeNascimento") %></td>
            <td><%# Eval("CPF") %></td>
            <td><%# Eval("Cidade") %></td>
            <td><%# Eval("Estado") %></td>
            <td><%# Eval("Endereco") %></td>
                     </tr>
                       </ItemTemplate>  
                    <FooterTemplate>
          </table>
                    </FooterTemplate>
        </asp:Repeater>     
    </main>
    
</asp:Content>
