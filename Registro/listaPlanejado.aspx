<%@ Page Title="Lista Planejado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="listaPlanejado.aspx.cs" Inherits="Registro.listaPlanejado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 44)
                return false;

            if (charCode == 46)
                return false;

            if (charCode == 39)
                return false;

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;

        }

        function Data(evt) {

            return false;

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="blocoGrupoCampos" style="margin-left: 10px; width: 900px;">
                <fieldset class="bordaFieldset" style="width: 870px;">
                    <legend>Registrar Planejamento</legend>
                    <div class="blocoeditor">
                        <label style="margin-left: 0px">
                            Produção</label>
                        <br />
                        <asp:DropDownList ID="cboProducao" Width="130px" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="cboProducao_SelectedIndexChanged">
                            <asp:ListItem Selected="True">SMT</asp:ListItem>
                            <asp:ListItem>BE</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBE_SMT" ValidationGroup="btnSalvar"
                            InitialValue="0" Text="*" ControlToValidate="cboProducao" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                    </div>
                    <div class="blocoGrupoCampos">
                        <div class="blocoeditor">
                            <label style="margin-left: 0px">
                                Modelo</label>
                            <br />
                            <asp:DropDownList ID="cboModelo" Width="150px" runat="server" AutoPostBack="True"
                                ValidationGroup="btnSalvar" OnSelectedIndexChanged="cboModelo_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="cboModelo" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px">
                            <label style="margin-left: 0px">
                                Valor planejado</label>
                            <br />
                            <asp:TextBox ID="txtValor" runat="server" Width="80px" AutoPostBack="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="btnSalvar"
                                Text="*" ControlToValidate="txtValor" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div class="blocoeditor" style="margin-left: 0px">
                            <label style="margin-left: 0px">
                                Horário</label>
                            <br />
                            <asp:DropDownList ID="cboHorario" Width="150px" runat="server">
                                <asp:ListItem Selected="True" Value="0">Selecione</asp:ListItem>
                                <asp:ListItem Value="00:00-01:00">00:00-01:00</asp:ListItem>
                                <asp:ListItem Value="01:00-02:00">01:00-02:00</asp:ListItem>
                                <asp:ListItem Value="02:00-03:00">02:00-03:00</asp:ListItem>
                                <asp:ListItem Value="03:00-04:00">03:00-04:00</asp:ListItem>
                                <asp:ListItem Value="04:00-05:00">04:00-05:00</asp:ListItem>
                                <asp:ListItem Value="05:00-06:00">05:00-06:00</asp:ListItem>
                                <asp:ListItem Value="06:00-07:00">06:00-07:00</asp:ListItem>
                                <asp:ListItem Value="07:00-08:00">07:00-08:00</asp:ListItem>
                                <asp:ListItem Value="08:00-09:00">08:00-09:00</asp:ListItem>
                                <asp:ListItem Value="09:00-10:00">09:00-10:00</asp:ListItem>
                                <asp:ListItem Value="10:00-11:00">10:00-11:00</asp:ListItem>
                                <asp:ListItem Value="11:00-12:00">11:00-12:00</asp:ListItem>
                                <asp:ListItem Value="12:00-13:00">12:00-13:00</asp:ListItem>
                                <asp:ListItem Value="13:00-14:00">13:00-14:00</asp:ListItem>
                                <asp:ListItem Value="14:00-15:00">14:00-15:00</asp:ListItem>
                                <asp:ListItem Value="15:00-16:00">15:00-16:00</asp:ListItem>
                                <asp:ListItem Value="16:00-17:00">16:00-17:00</asp:ListItem>
                                <asp:ListItem Value="17:00-18:00">17:00-18:00</asp:ListItem>
                                <asp:ListItem Value="18:00-19:00">18:00-19:00</asp:ListItem>
                                <asp:ListItem Value="19:00-20:00">19:00-20:00</asp:ListItem>
                                <asp:ListItem Value="20:00-21:00">20:00-21:00</asp:ListItem>
                                <asp:ListItem Value="21:00-22:00">21:00-22:00</asp:ListItem>
                                <asp:ListItem Value="22:00-23:00">22:00-23:00</asp:ListItem>
                                <asp:ListItem Value="23:00-24:00">23:00-24:00</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="btnSalvar"
                                InitialValue="0" Text="*" ControlToValidate="cboHorario" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                        </div>
                        <div id="divLinhaSMT" runat="server" class="blocoGrupoCampos" style="margin-left: 10px;">
                            <fieldset class="bordaFieldset" style="width: 150px;">
                                <legend>Linha</legend>
                                <div class="blocoGrupoCampos">
                                    <div class="blocoeditor">
                                        <%-- <label style="margin-left: 0px">
                                    Linha</label>
                                <br />--%>
                                        <%--  <asp:RadioButtonList ID="rdbListaLinha" runat="server">
                                </asp:RadioButtonList>--%>
                                        <asp:CheckBoxList ID="chkListaLinha" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div id="divLadoSMT" runat="server" class="blocoGrupoCampos" style="margin-left: 10px;">
                            <fieldset class="bordaFieldset" style="width: 150px;">
                                <legend>Lado</legend>
                                <div class="blocoGrupoCampos">
                                    <div class="blocoeditor">
                                        <asp:RadioButtonList ID="rdbLado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdbLado_SelectedIndexChanged">
                                            <asp:ListItem Value="0">BOT</asp:ListItem>
                                            <asp:ListItem Value="1">TOP</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="btnSalvar"
                                            InitialValue="" Text="*" ControlToValidate="rdbLado" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div id="divBE" runat="server" class="blocoGrupoCampos" style="margin-left: 0px;">
                            <div class="blocoeditor">
                                <label style="margin-left: 0px">
                                    Lado</label>
                                <br />
                                <asp:DropDownList ID="cboLado" Width="130px" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="cboLado_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="btnSalvar"
                                    InitialValue="0" Text="*" ControlToValidate="cboLado" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                            </div>
                            <div class="blocoeditor">
                                <label style="margin-left: 0px">
                                    Linha</label>
                                <br />
                                <asp:DropDownList ID="cboLinha"  Width="150px" runat="server" AutoPostBack="True"
                                    value="0">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="btnSalvar"
                                    Text="*" ControlToValidate="cboLinha" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="blocoGrupoCampos">
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
                        BackColor="White" BorderColor="#CCCCCC" OnRowCommand="gridDados_RowCommand">
                        <EmptyDataTemplate>
                            Sem registro.
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="modelo" HeaderText="Modelo">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Planejado L1">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorL1" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="btnEditar"
                                        Text="*" ControlToValidate="txtValorL1" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planejado L2" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorL2" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="btnEditar"
                                        Text="*" ControlToValidate="txtValorL2" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planejado L3">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorL3" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="btnEditar"
                                        Text="*" ControlToValidate="txtValorL3" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planejado L4" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorL4" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="btnEditar"
                                        Text="*" ControlToValidate="txtValorL4" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Planejado L5" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorL5" runat="server" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="btnEditar"
                                        Text="*" ControlToValidate="txtValorL5" ForeColor="Red" runat="server"> </asp:RequiredFieldValidator>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="10px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="horario" HeaderText="Horário">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="10px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lado" HeaderText="Lado">
                                <ItemStyle HorizontalAlign="Center" BorderWidth="3px" Width="10px" Font-Bold="True" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" ImageUrl="~/imagens/ic_ok.png"
                                        CommandName="editar" ToolTip="Editar" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                        Style="text-decoration: underline; top: 0px; left: 2px; height: 22px; width: 28px"
                                        OnClientClick='return confirm("Confirmar Alteração?");' ValidationGroup="btnEditar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/imagens/icon/close.png"
                                        CommandName="remover" ToolTip="Excluir" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                        Style="text-decoration: underline;" OnClientClick='return confirm("Confirmar Exclusão?");' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderWidth="5px" Width="10px" />
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
