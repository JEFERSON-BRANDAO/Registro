<%@ Page Title="Aniversariente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="aniversariante.aspx.cs" Inherits="Registro.aniversariante" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        jQuery.noConflict();
        (function ($) {
            $(function () {
                $('.mask-data').mask('99/99/9999');
            });
        })(jQuery);

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="blocoGrupoCampos" style="margin-left: 10px; width: 900px;">
                <fieldset class="bordaFieldset" style="width: 870px;">
                    <legend>Aniversáriante</legend>
                    <div class="blocoGrupoCampos">
                        <div class="blocoeditor">
                            <label style="margin-left: 0px">
                                Setor</label>
                            <br />
                            <asp:DropDownList ID="cboSetor" Width="150px" runat="server">
                                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                                <asp:ListItem>SMT</asp:ListItem>
                                <asp:ListItem>BE</asp:ListItem>
                                <asp:ListItem>PTH</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="btnSalvar"
                                InitialValue="0" Text="*" ControlToValidate="cboSetor" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px">
                            <label style="margin-left: 0px">
                                Matrícula</label>
                            <br />
                            <asp:TextBox ID="txtMatricula" runat="server" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="txtMatricula" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px">
                            <label style="margin-left: 0px">
                                Nome</label>
                            <br />
                            <asp:TextBox ID="txtNome" runat="server" Width="350px" MaxLength="25"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="txtNome" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px">
                            <label style="margin-left: 0px">
                                Data Aniversário</label>
                            <br />
                            <asp:TextBox ID="txtDataAniversario" runat="server" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="txtDataAniversario" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                            <asp:CalendarExtender ID="clnDataAniversario" runat="server" TargetControlID="txtDataAniversario"
                                meta:resourcekey="txtDataAniversarioResource1" TodaysDateFormat="dd MMMM, yyyy"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorDataAniversario" runat="server"
                                ValidationGroup="btnSalvar" ErrorMessage="Data Inválida." ControlToValidate="txtDataAniversario"
                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}$"
                                ForeColor="#CC0000"></asp:RegularExpressionValidator>
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
                                    ForeColor="Black" Text="          Voltar " ToolTip="Voltar" 
                                    onclick="btnVoltar_Click" />
                            </div>
                        </div>
                        <div class="blocoeditor">
                            <asp:Label ID="lbAviso" runat="server" Visible="False" ForeColor="Red" meta:resourcekey="lbAvisoResource1"></asp:Label>
                        </div>
                    </div>
                </fieldset>
                <div class="blocoGrupoCampos">
                    <asp:GridView ID="gridDados" Width="900px" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="matricula"
                        BackColor="White" BorderColor="#CCCCCC" AllowPaging="True" OnRowCommand="gridDados_RowCommand"
                        OnPageIndexChanging="gridDados_PageIndexChanging">
                        <EmptyDataTemplate>
                            Sem registro.
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Setor">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cboSetor" runat="server" Width="150px" value="" ValidationGroup="btnEditar">
                                        <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                                        <asp:ListItem>SMT</asp:ListItem>
                                        <asp:ListItem>BE</asp:ListItem>
                                        <asp:ListItem>PTH</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="btnEditar"
                                        InitialValue="0" Text="*" ControlToValidate="cboSetor" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="160px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Matrícula">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMatricula" Text='<%# Eval("matricula") %>' runat="server" Width="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="btnEditar"
                                        Text="*" ControlToValidate="txtMatricula" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="90px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nome">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNome" Text='<%# Eval("nome") %>' runat="server"  MaxLength="25" Width="400px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="btnEditar"
                                        Text="*" ControlToValidate="txtNome" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                    <br />
                                    <br />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="320px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data Aniversário">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDataAniversario" Text='<%# Eval("dataaniversario") %>' runat="server"
                                        Width="80px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="btnEditar"
                                        Text="*" ControlToValidate="txtDataAniversario" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                    <asp:CalendarExtender ID="clnDataAniversario2" runat="server" TargetControlID="txtDataAniversario"
                                        meta:resourcekey="txtDataAniversario2Resource1" TodaysDateFormat="dd MMMM, yyyy"
                                        Format="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorDataAniversario2" runat="server"
                                        ValidationGroup="btnEditar" ErrorMessage="Data Inválida." ControlToValidate="txtDataAniversario"
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}$"
                                        ForeColor="#CC0000"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="95px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Setor" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbSetor" runat="server" Text='<%# Eval("setor") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="320px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" ValidationGroup="btnEditar" runat="server" AlternateText="Editar"
                                        ImageUrl="~/imagens/ic_ok.png" CommandName="editar" ToolTip="Editar" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                        Style="text-decoration: underline; top: 0px; left: 2px; height: 22px; width: 28px"
                                        OnClientClick='return confirm("Confirmar Alteração?");' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/imagens/icon/close.png"
                                        CommandName="remover" ToolTip="Excluir" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                        Style="text-decoration: underline;" OnClientClick='return confirm("Confirmar Exclusão?");' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="20px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
