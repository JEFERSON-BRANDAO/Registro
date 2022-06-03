<%@ Page Title="Lista Observacao" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="listaObservacao.aspx.cs" Inherits="Registro.listaObservacao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%-- CONTAR CARACTERES campo MOTIVOCOMPRA --%>
    <script type="text/javascript">
        function contaLetras() {

            var maxlimit = 100;
            // pega o label
            var label = document.getElementById('<%=lbRestantes.ClientID %>');
            // paga a textbox
            var textbox = document.getElementById('<%=txtObservacao.ClientID %>');

            // passou do limite?
            if (textbox.value.length > maxlimit)
            // trim!
                textbox.value = textbox.value.substring(0, maxlimit);
            else // conta
                label.innerText = (maxlimit - textbox.value.length) + " caracteres restantes";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="blocoGrupoCampos" style="margin-left: 10px; width: 900px;">
                <fieldset class="bordaFieldset" style="width: 870px;">
                    <legend>Registrar Observação</legend>
                    <div class="blocoeditor">
                        <label style="margin-left: 0px">
                            Produção</label>
                        <br />
                        <asp:DropDownList ID="cboProducao" Width="130px" runat="server" 
                            AutoPostBack="True" onselectedindexchanged="cboProducao_SelectedIndexChanged">
                            <asp:ListItem Selected="True">SMT</asp:ListItem>
                            <asp:ListItem>BE</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBE_SMT" ValidationGroup="btnSalvar"
                            InitialValue="0" Text="*" ControlToValidate="cboProducao" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                    </div>
                    <div class="blocoGrupoCampos" style="margin-left: 10px">
                        <fieldset class="bordaFieldset" style="width: 150px;">
                            <legend>Tipo de Observação</legend>
                            <asp:RadioButtonList ID="rdbtTipoObs" runat="server">
                                <asp:ListItem Selected="True" Value="0">Eficiência</asp:ListItem>
                                <asp:ListItem Value="1">Yield Rate</asp:ListItem>
                            </asp:RadioButtonList>
                        </fieldset>
                    </div>
                    <div class="blocoGrupoCampos">
                        <div class="blocoeditor">
                            <label style="margin-left: 0px">
                                Modelo</label>
                            <br />
                            <asp:DropDownList ID="cboModelo"  Width="150px" runat="server" AutoPostBack="True"
                                ValidationGroup="btnSalvar" value="0" OnSelectedIndexChanged="cboModelo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="cboModelo" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px">
                            <label style="margin-left: 0px">
                                Data</label>
                            <br />
                            <asp:TextBox ID="txtDataProducao" Enabled="false" runat="server" Width="80px" AutoPostBack="True"
                                OnTextChanged="txtDataProducao_TextChanged"></asp:TextBox>
                            <asp:CalendarExtender ID="clnDataProducao" runat="server" TargetControlID="txtDataProducao"
                                meta:resourcekey="txtDataProducaoResource1" TodaysDateFormat="dd MMMM, yyyy"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorDataProducao" runat="server"
                                ValidationGroup="btnSalvar" ErrorMessage="Data Inválida." ControlToValidate="txtDataProducao"
                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}$"
                                ForeColor="#CC0000"></asp:RegularExpressionValidator>
                        </div>
                        <div class="blocoeditor">
                            <label style="margin-left: 0px">
                                Lado</label>
                            <br />
                            <asp:DropDownList ID="cboLado" Enabled="false" Width="130px" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="cboLado_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="btnSalvar"
                                InitialValue="0" Text="*" ControlToValidate="cboLado" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="blocoeditor">
                            <label style="margin-left: 0px">
                                Linha</label>
                            <br />
                            <asp:DropDownList ID="cboLinha" Enabled="false" Width="150px" runat="server" AutoPostBack="True"
                                value="0" OnSelectedIndexChanged="cboLinha_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="cboLinha" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="blocoeditor">
                            <label style="margin-left: 0px">
                                Horário</label>
                            <br />
                            <asp:DropDownList ID="cboHorario" Enabled="false" Width="150px" runat="server" AutoPostBack="True"
                                value="0">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="cboHorario" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="blocoGrupoCampos">
                        <div class="blocoeditor">
                            <label style="margin-left: 0px">
                                Observação</label>
                            <br />
                            <asp:TextBox ID="txtObservacao" runat="server" Visible="true" TextMode="MultiLine"
                                onKeyDown="contaLetras();" onKeyUp="contaLetras();" Width="180px" Height="150px"
                                MaxLength="99"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="txtObservacao" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                            <asp:TextBoxWatermarkExtender ID="twFname" runat="server" TargetControlID="txtObservacao"
                                WatermarkText="Digite aqui o motivo">
                            </asp:TextBoxWatermarkExtender>
                            <br />
                            <asp:Label ID="lbRestantes" runat="server" Visible="true" Text="100 caracteres restantes"></asp:Label>
                        </div>
                        <div class="blocoeditor">
                            <asp:Label ID="lbAviso" runat="server" Visible="False" ForeColor="Red" meta:resourcekey="lbAvisoResource1"></asp:Label>
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
                    <asp:GridView ID="gridDados" Width="900px" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="modelo"
                        BackColor="White" BorderColor="#CCCCCC" OnRowCommand="gridDados_RowCommand" AllowPaging="True"
                        OnPageIndexChanging="gridDados_PageIndexChanging">
                        <EmptyDataTemplate>
                            Sem registro.
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="modelo" HeaderText="Modelo">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="100px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lado" HeaderText="Lado">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="linha" HeaderText="Linha">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="horario" HeaderText="Horário">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="100px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dataProducao" HeaderText="Dt.Produção">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Observação">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtObservacao" Text='<%# Eval("observacao") %>' runat="server" Height="60px"
                                        ReadOnly="false" TextMode="MultiLine"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="70px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="tipo_obs" HeaderText="Tipo Obs">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <%--       '<%#Eval("modelo") + "," + Eval("lado") + "," + Eval("linha")  + "," + Eval("horario") + "," + Eval("dataProducao") %>'--%>
                                    <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" ImageUrl="~/imagens/ic_ok.png"
                                        CommandName="editar" ToolTip="Editar" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
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
