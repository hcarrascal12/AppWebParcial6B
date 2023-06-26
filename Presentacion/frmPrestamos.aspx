<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmPrestamos.aspx.cs" Inherits="Presentacion.frmPrestamos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Prestamos
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row ">
            <div class="col-lg-12 col-md-12">
                <div class="card">
                    <div class="card-header card-header-text">
                        <h4 class="card-title"><i class="fa-solid fa-book me-2"></i>Prestamos</h4>
                    </div>
                    <div class="card-content table-responsive">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb d-flex justify-content-end">
                                <li class="breadcrumb-item">
                                    <asp:HyperLink NavigateUrl="~/frmAdmin.aspx" runat="server"><i class="fa-solid fa-home"></i>
                                    Inicio</asp:HyperLink>
                                </li>
                                <li class="breadcrumb-item active" aria-current="page">Prestamos</li>
                            </ol>
                        </nav>

                        <div class="row">
                            <div class="col-lg-4 col-md-12">
                                <label class="form-label">Estado prestado</label>
                                <asp:DropDownList CssClass="form-control" ID="cmbEstadoPrestamo" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-lg-4 col-md-12">
                                <label class="form-label">Lectores</label>
                                <asp:DropDownList CssClass="form-control" ID="DropDownList2" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-lg-4 col-md-12">
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server">
                                    <i class="fa-solid fa-search"></i> Buscar
                                </asp:LinkButton>
                            </div>
                        </div>

                        <div class="table small">
                            <asp:GridView ID="gvPrestamos" ShowHeaderWhenEmpty="True" CssClass="tblPrestamos table table-striped table-responsive" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" />
                                    <asp:BoundField DataField="EstadoPrestamo" HeaderText="Estado Préstamo" />
                                    <asp:BoundField DataField="N_ide" HeaderText="Nit Lector" />
                                    <asp:BoundField DataField="NombreLibro" HeaderText="Título Libro" />
                                    <asp:BoundField DataField="FechaDevolucion" HeaderText="Fecha Devolución" />
                                    <asp:BoundField DataField="FechaConfirmacionDevolucion" HeaderText="Fecha Devolución Confirmado" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="OpenEditModal" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary" title="Devolver">
                                                <i class="fa-solid fa-refresh"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

