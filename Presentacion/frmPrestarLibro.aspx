<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmPrestarLibro.aspx.cs" Inherits="Presentacion.frmPrestarLibro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Prestar libro
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row ">
            <div class="col-lg-12 col-md-12">
                <div class="card">
                    <div class="card-header card-header-text">
                        <h4 class="card-title">Prestar libro</h4>
                    </div>
                    <div class="card-content">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb d-flex justify-content-end">
                                <li class="breadcrumb-item"><asp:HyperLink NavigateUrl="~/frmAdmin.aspx" runat="server"><i class="fa-solid fa-home"></i>
                                    Inicio</asp:HyperLink></li>
                                <li class="breadcrumb-item"><a href="#"><i class="fa-solid fa-handshake"></i>
                                    Prestar libro</a></li>
                            </ol>
                        </nav>

                        <div class="row">
                            <div class="col-lg-6 col-md-12 col-sm-12">
                                <div class="border p-3">
                                    <h4>Lector</h4>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label class="form-label">Nit</label>
                                            <div class="input-group mb-3">
                                                <asp:Label ID="lblIdLector" runat="server" Text="Label" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtNitLector" class="form-control" runat="server"></asp:TextBox>
                                                <asp:LinkButton ID="btnAbrirModalLector" OnClick="btnAbrirModalLector_Click" CssClass="btn btn-primary" runat="server">
                                                <i class="fa-solid fa-search"></i> Buscar
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="form-label">Nombre</label>
                                            <asp:TextBox ID="txtNombreLector" class="form-control" disabled runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6 col-md-12 col-sm-12">
                                <div class="border p-3">
                                    <h4>Libro</h4>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label class="form-label">ISBN</label>
                                            <div class="input-group mb-3">
                                                <asp:Label ID="lblIdLibro" runat="server" Text="" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtIsbn" class="form-control" runat="server"></asp:TextBox>
                                                <asp:LinkButton ID="btnAbrirModalLibro" OnClick="btnAbrirModalLibro_Click" CssClass="btn btn-primary" runat="server">
                                                <i class="fa-solid fa-search"></i> Buscar
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="form-label">Titulo</label>
                                            <asp:TextBox ID="txtNombreLibro" class="form-control" disabled runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div class="border p-3">
                                    <h4>Fecha de devolución</h4>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <label class="form-label">Fecha de devolución (DD/MM/YYYY)</label>
                                            <div class="d-flex justify-content-center">
                                                <!--<asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>-->
                                                <asp:TextBox ID="txtFechaDevolucion" CssClass="form-control datepicker" runat="server" TextMode="DateTime" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="d-flex justify-content-center mt-3">
                            <asp:Button ID="btnRegistrarPrestamo" OnClick="btnRegistrarPrestamo_Click" runat="server" CssClass="btn btn-primary me-2" Text="Registrar préstamo" />
                            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelar_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Buscar Libro -->
    <div class="modal fade" id="mdlLibro" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h1 class="modal-title text-white fs-5" id="exampleModalLabel">Buscar libro</h1>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvLibros" OnRowCommand="gvLibros_RowCommand" ShowHeaderWhenEmpty="True" CssClass="tblLibros table table-striped table-responsive" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="isbn" HeaderText="ISBN" />
                            <asp:BoundField DataField="nombre" HeaderText="Título" />
                            <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEscogerLibro" runat="server" CommandName="EscogerLibro" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary" title="Escoger">
                                                <i class="fa-solid fa-check"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">

                    <asp:Button ID="btnCerrarModal" class="btn btn-secondary" runat="server" Text="Cancelar" data-bs-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Buscar Lector -->
    <div class="modal fade" id="mdlLector" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h1 class="modal-title text-white fs-5" id="exampleModalLabel">Buscar Lector</h1>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvLector" OnRowCommand="gvLector_RowCommand" ShowHeaderWhenEmpty="True" CssClass="tblLector table table-striped table-responsive" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="n_ide" HeaderText="Nit" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEscogerLector" runat="server" CommandName="EscogerLector" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-primary" title="Escoger">
                                                <i class="fa-solid fa-check"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">

                    <asp:Button ID="Button1" class="btn btn-secondary" runat="server" Text="Cancelar" data-bs-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

    <!--Modal confirmar prestamos-->
    <div class="modal fade" id="mdlConfirmarPrestamo" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h1 class="modal-title text-white fs-5" id="exampleModalLabel">Confirmar préstamo</h1>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label class="form-label">Estado de libro entregado</label>
                        <textarea id="txtObservacion" runat="server" class="form-control"></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnConfirmarPrestamo" OnClick="btnConfirmarPrestamo_Click" class="btn btn-primary" runat="server" Text="Registrar préstamo"/>
                    <asp:Button ID="btnVolver" class="btn btn-danger" runat="server" Text="Volver" data-bs-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
