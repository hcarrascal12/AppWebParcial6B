<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmLibros.aspx.cs" Inherits="Presentacion.frmLibros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Libros
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row ">
            <div class="col-lg-12 col-md-12">
                <div class="card">
                    <div class="card-header card-header-text">
                        <h4 class="card-title"><i class="fa-solid fa-book me-2"></i>Libros</h4>
                    </div>
                    <div class="card-content table-responsive">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb d-flex justify-content-end">
                                <li class="breadcrumb-item">
                                    <asp:HyperLink NavigateUrl="~/frmAdmin.aspx" runat="server"><i class="fa-solid fa-home"></i>
                                    Inicio</asp:HyperLink>
                                </li>
                                <li class="breadcrumb-item active" aria-current="page">Libros</li>
                            </ol>
                        </nav>
                        <asp:LinkButton ID="btnCrearLibro" OnClick="btnCrearLibro_Click" CssClass="btn btn-primary mb-4 me-2" runat="server">
                            <i class="fa-solid fa-plus"></i> Crear Libro
                        </asp:LinkButton>
                        <div class="table small">
                            <asp:GridView ID="gvLibros" ShowHeaderWhenEmpty="True" CssClass="tblLibros table table-striped table-responsive" runat="server" AutoGenerateColumns="false" OnRowCommand="gvLibros_RowCommand" OnRowDataBound="gvLibros_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" />
                                    <asp:BoundField DataField="isbn" HeaderText="Isbn" />
                                    <asp:BoundField DataField="nombre" HeaderText="Título" />
                                    <asp:BoundField DataField="autor" HeaderText="Autor" />
                                    <asp:BoundField DataField="categoria" HeaderText="Categoria" />
                                    <asp:BoundField DataField="editorial" HeaderText="Editorial" />
                                    <asp:BoundField DataField="edicion" HeaderText="Edicion" />
                                    <asp:BoundField DataField="disponible" HeaderText="Disponible" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" OnClick="btnEditar_Click" runat="server" CssClass="btn btn-warning" title="Editar libro">
                                                <i class="fa-solid fa-edit text-white"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton  ID="btnEliminar" CausesValidation="false"  CommandName="OpenModal" CommandArgument='<%# Eval("id") %>' runat="server" CssClass="btn btn-danger"  title="Eliminar libro">
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
    <!-- Modal -->
    <div class="modal fade" id="mdlEliminarLibro" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h1 class="modal-title text-white fs-5" id="exampleModalLabel">Eliminar libro</h1>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblID" runat="server" Visible="False" />
                    <p class="text-center">¿Estás seguro de eliminar a este libro?</p>
                </div>
                <div class="modal-footer">

                    <asp:Button ID="btnCerrarModal" OnClick="btnCerrarModal_Click" class="btn btn-secondary" runat="server" Text="Cancelar" data-bs-dismiss="modal" />
                    <asp:Button ID="btnEliminarLibro" OnClick="btnEliminarLibro_Click" class="btn btn-danger" runat="server" Text="Eliminar libro"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

