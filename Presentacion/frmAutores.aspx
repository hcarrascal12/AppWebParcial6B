<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmAutores.aspx.cs" Inherits="Presentacion.frmAutores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Autores
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row ">
            <div class="col-lg-12 col-md-12">
                <div class="card">
                    <div class="card-header card-header-text">
                        <h4 class="card-title"><i class="fa-solid fa-book me-2"></i>Autores</h4>
                    </div>
                    <div class="card-content table-responsive">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb d-flex justify-content-end">
                                <li class="breadcrumb-item">
                                    <asp:HyperLink NavigateUrl="~/frmAdmin.aspx" runat="server"><i class="fa-solid fa-home"></i>
                                    Inicio</asp:HyperLink>
                                </li>
                                <li class="breadcrumb-item active" aria-current="page">Autores</li>
                            </ol>
                        </nav>
                        <asp:LinkButton ID="btnCrear" CssClass="btn btn-primary mb-4 me-2" OnClick="btnCrear_Click" runat="server">
                            <i class="fa-solid fa-plus"></i> Crear autor
                        </asp:LinkButton>

                        <div class="table small">


                            <asp:GridView ID="gvAutores" ShowHeaderWhenEmpty="True" EmptyDataRowStyle-CssClass="text-center" AllowPaging="true" PageSize="10" EmptyDataText="No hay datos disponibles" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="false" OnRowCommand="gvAutores_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="OpenEditModal" CommandArgument="<%# Container.DataItemIndex %>" CssClass="btn btn-warning" title="Editar categoría">
                                                <i class="fa-solid fa-edit text-white"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton  ID="btnEliminar" CausesValidation="false"  CommandName="OpenModal" CommandArgument='<%# Eval("id") %>' runat="server" CssClass="btn btn-danger"  title="Eliminar categoría">
                                                <i class="fa-solid fa-times text-white"></i>
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

    <!-- Modal Crear/Editar -->
    <div class="modal fade" id="mdlAutores" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h1 class="modal-title text-white fs-5" runat="server" id="lblTituloModal"></h1>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblId" runat="server" Visible="False" />
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="Label1" runat="server" class="form-label" Text="Descripcion"></asp:Label>
                            <asp:TextBox ID="txtDescripcion" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseModal" OnClick="btnCloseModal_Click" class="btn btn-secondary" runat="server" Text="Cancelar" data-bs-dismiss="modal"/>
                    <asp:Button ID="btnCrearAutor" OnClick="btnCrearAutor_Click" class="btn btn-primary" runat="server" Text="Crear autor" Visible="False" />
                    <asp:Button ID="btnActualizarAutor" OnClick="btnActualizarAutor_Click" class="btn btn-warning text-white" runat="server" Text="Actualizar autor" Visible="False" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Eliminar -->
    <div class="modal fade" id="mdlEliminarAutor" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h1 class="modal-title text-white fs-5" id="exampleModalLabel">Eliminar autor</h1>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblIdEliminar" runat="server" Visible="False" />
                    <p class="text-center">¿Estás seguro de eliminar a este autor?</p>
                </div>
                <div class="modal-footer">

                    <asp:Button ID="btnCerrarModal" OnClick="btnCerrarModal_Click"  class="btn btn-secondary" runat="server" Text="Cancelar" data-bs-dismiss="modal" />
                    <asp:Button ID="btnEliminarAutor" OnClick="btnEliminarAutor_Click" class="btn btn-danger" runat="server" Text="Eliminar autor" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>



