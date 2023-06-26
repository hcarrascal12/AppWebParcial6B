<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmUsuarios.aspx.cs" Inherits="Presentacion.frmUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Usuarios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row ">
            <div class="col-lg-12 col-md-12">
                <div class="card">
                    <div class="card-header card-header-text">
                        <h4 class="card-title"><i class="fa-solid fa-users me-2"></i>Usuarios</h4>
                    </div>
                    <div class="card-content table-responsive">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb d-flex justify-content-end">
                                <li class="breadcrumb-item">
                                    <asp:HyperLink NavigateUrl="~/frmAdmin.aspx" runat="server"><i class="fa-solid fa-home"></i>
                                    Inicio</asp:HyperLink>
                                </li>
                                <li class="breadcrumb-item active" aria-current="page">Usuarios</li>
                            </ol>
                        </nav>
                        <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear usuario" CssClass="btn btn-primary mb-4 me-2" OnClick="btnCrearUsuario_Click"/>

                        <div class="table small">


                            <asp:GridView ID="gvUsuarios" ShowHeaderWhenEmpty="True" CssClass="tblUsuarios table table-striped table-responsive" runat="server" AutoGenerateColumns="false" OnRowCommand="gvUsuarios_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" />
                                    <asp:BoundField DataField="username" HeaderText="Usuario" />
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                    <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                                    <asp:BoundField DataField="email" HeaderText="Correo" />
                                    <asp:BoundField DataField="tipo usuario" HeaderText="Rol" />
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_Click" CssClass="btn btn-warning" title="Editar usuario">
                                                <i class="fa-solid fa-edit text-white"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton  ID="btnEliminar" CausesValidation="false"  CommandName="OpenModal" CommandArgument='<%# Eval("id") %>' runat="server" CssClass="btn btn-danger"  title="Eliminar usuario">
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
    <div class="modal fade" id="mdlEliminarUsuario" tabindex="-1" data-bs-backdrop="static" data-bs-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h1 class="modal-title text-white fs-5" id="exampleModalLabel">Eliminar usuario</h1>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblID" runat="server" Visible="False" />
                    <p class="text-center">¿Estás seguro de eliminar a este usuario?</p>
                </div>
                <div class="modal-footer">

                    <asp:Button ID="btnCerrarModal" class="btn btn-secondary" runat="server" Text="Cancelar" data-bs-dismiss="modal" OnClick="btnCerrarModal_Click" />
                    <asp:Button ID="btnEliminarUsuario" class="btn btn-danger" runat="server" Text="Eliminar usuario" OnClick="btnEliminarUsuario_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
