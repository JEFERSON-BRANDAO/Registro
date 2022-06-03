<%@ Page Title="Grafico" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="grafico.aspx.cs" Inherits="Registro.grafico" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="blocoGrupoCampos" style="margin-left: 10px; width: 900px;">
                <fieldset class="bordaFieldset" style="width: 870px;">
                    <legend>Inserir imagem de gráfico</legend>
                    <div class="blocoGrupoCampos">
                        <fieldset class="bordaFieldset" style="width: 240px; margin-left: 0px">
                            <legend>Local</legend>
                            <div class="blocoeditor">
                                <asp:RadioButtonList ID="rdBtnLocal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdBtnLocal_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="FE">FE</asp:ListItem>
                                    <asp:ListItem Value="BE">BE</asp:ListItem>
                                    <asp:ListItem>RMA</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Label ID="lbLocal" runat="server" Visible="true"></asp:Label>
                            </div>
                        </fieldset>
                    </div>
                    <div class="blocoGrupoCampos" style="margin-left: -10px">
                        <div class="blocoeditor">
                            <label style="margin-left: 0px">
                                Modelo</label>
                            <br />
                            <asp:DropDownList ID="cboModelo" Width="150px" runat="server" ValidationGroup="btnSalvar"
                                value="0" AutoPostBack="True" 
                                onselectedindexchanged="cboModelo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="cboModelo" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="blocoGrupoCampos">
                        <fieldset class="bordaFieldset" style="width: 253px; margin: 0px; padding: 17px;
                            padding-top: 2px">
                            <legend>Tem imagem?</legend>
                            <div class="blocoGrupoCampos">
                                <asp:Label ID="lbStatusImagem" runat="server" Text="-" Font-Bold="True"></asp:Label>
                            </div>
                        </fieldset>
                    </div>
                    <div class="blocoGrupoCampos">
                        <fieldset class="bordaFieldset" style="width: 240px; margin: 0px; padding: 17px;
                            padding-top: 2px">
                            <legend>Imagem Gráfico</legend>
                            <div class="blocoGrupoCampos">
                                <br />
                            </div>
                            <div class="blocoGrupoCampos">
                                <asp:FileUpload ID="uplImagem" runat="server" />
                            </div>
                        </fieldset>
                    </div>
                    <div class="blocoGrupoCampos">
                        <div class="blocoeditor">
                            <asp:Label ID="lbAviso" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="blocoGrupoCampos" style="margin-left: 0px;">
                        <div class="blocoeditor" style="margin-left: 0px;">
                            <asp:Button ID="btnSalvar" ValidationGroup="btnSalvar" runat="server" Width="120px"
                                Height="40px" Style="background-image: url(/imagens/ic_ok.png); background-repeat: no-repeat;
                                background-position: center;" BackColor="White" ForeColor="Black" OnClientClick='return confirm("Tem certeza que deseja SALVAR?");'
                                Text="        Salvar" ToolTip="Salvar" OnClick="btnSalvar_Click" />
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px;">
                            <asp:Button ID="btnVoltar" runat="server" Width="120px" Height="40px" Style="background-image: url(/imagens/icon/back.png);
                                background-repeat: no-repeat; background-position: center;" BackColor="White"
                                ForeColor="Black" Text="          Voltar " ToolTip="Voltar" OnClick="btnVoltar_Click" />
                        </div>
                    </div>
                </fieldset>
                <div class="blocoGrupoCampos">
                    <asp:Image ID="imgGrafico" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSalvar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
