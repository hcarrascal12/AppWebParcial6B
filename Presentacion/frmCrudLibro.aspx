<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="frmCrudLibro.aspx.cs" Inherits="Presentacion.frmCrudLibro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <%#  GetTitulo()%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row ">
            <div class="col-lg-12 col-md-12">
                <div class="card">
                    <div class="card-header card-header-text">
                        <asp:Label ID="lblTitulo" CssClass="card-title" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="card-content">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb d-flex justify-content-end">
                                <li class="breadcrumb-item"><a href="#"><i class="fa-solid fa-home"></i>
                                    Inicio</a></li>
                                <li class="breadcrumb-item"><a href="#"><i class="fa-solid fa-book"></i>
                                    Libros</a></li>
                                <li class="breadcrumb-item active" aria-current="page"><%# GetTitulo() %></li>
                            </ol>
                        </nav>

                        <div class="row my-2">
                            <div class="col-md-6">
                                <label class="form-label">Isbn</label>
                                <asp:TextBox ID="txtIsbn" class="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Titulo</label>
                                <asp:TextBox ID="txtNombre" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row my-2">
                            <div class="col-md-6">
                                 <label class="form-label">Autores</label>
                                 <asp:DropDownList ID="cmbAutores" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                 <label class="form-label">Edicion</label>
                                 <asp:DropDownList ID="cmbEdicion" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="row my-2">
                            <div class="col-md-6">
                                 <label class="form-label">Editorial</label>
                                 <asp:DropDownList ID="cmbEditorial" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label class="form-label">Categoría</label>
                                <asp:DropDownList ID="cmbCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="d-flex justify-content-center">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary me-2" Text="Crear libro" Visible="False" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-primary me-2" Text="Actualizar libro" Visible="False" OnClick="btnActualizar_Click" />
                            <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-danger" Text="Volver" OnClick="btnVolver_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

